

using AutoMapper;
using AutoMapper.QueryableExtensions;
using ElenSoft.Application.Repository.V1.IService;
using ElenSoft.Application.ViewModels;
using ElenSoft.Application.ViewModels.Identity.User.Cmd;
using ElenSoft.Application.ViewModels.Identity.User.Query;
using ElenSoft.DataLayer.Models.Context;
using ElenSoft.DataLayer.Models.Entities;
using ElenSoft.Insfrastrcture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.Repository.V1.Services

{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly HttpContext _httpContext;
        private readonly AppDBContext _context;

        public UserService(UserManager<ApplicationUser> userManager,
                                IHttpContextAccessor httpContextAccessor,
                                RoleManager<ApplicationRole> roleManager,
                                IConfiguration configuration,
                               AppDBContext context, IMapper mapper)
        {
            _mapper = mapper;
            _httpContext = httpContextAccessor.HttpContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }

        #region ChangePassword
        public async Task<Response> ChangePassword(ChangePasswordCmd request)
        {
            var userId = _httpContext.GetUserId();
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
            if (!userHasValidPassword)
            {
                throw new BusinessLogicException("رمز عبور صحیح نمی باشد");
            }

            var hashPassword = _userManager.PasswordHasher.HashPassword(user, request.NewPassword);
            user.PasswordHash = hashPassword;
            await _userManager.UpdateAsync(user);

            return new Response
            {
                Status = true,
                Message = "success"

            };
        }
        #endregion

        #region delete user
        public async Task<Response> DeleteUser(string id)
        {
            var user = await _userManager.Users.FirstAsync(z => z.Id == id);
            if (user == null)
            {
                throw new BusinessLogicException("رکوردی با این مشخصات یافت نشد");
            }
            await _userManager.DeleteAsync(user);
            return new Response
            {
                Status = true,
                Message = "success"

            };
        }
        #endregion

        #region GetUser
        public async Task<Response<UserDto>> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new BusinessLogicException("رکوردی با این مشخصات یافت نشد");
            }
            var item = new UserDto();
            item = _mapper.Map<UserDto>(user);

            return new Response<UserDto>
            {
                Data = item,
                Status = true,
                Message = "success"

            };
        }
        #endregion

        #region GetUsers
        public async Task<Response<UsersDto>> GetUsers(UsersQuery request)
        {
            var result = _userManager.Users.AsQueryable();
            var finalResult = await result.ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync();
            var resultData = new UsersDto
            {
                Dtos = finalResult,
                PageId = request.PageId,
                PageSize = request.PageSize,
                Total = await result.CountAsync()
            };
            return new Response<UsersDto>
            {
                Data = resultData,
                Status = true,
                Message = "success"
            };


        }
        #endregion


        #region login
        public async Task<Response<AuthenticationToken>> LoginUser(LoginCmd request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new BusinessLogicException("رکوردی با این مشخصات یافت نشد");
            }
            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!userHasValidPassword)
            {
                throw new BusinessLogicException("نام کاربری و یا کلمه عبور را درست وارد نکرده اید");

            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var userRole = userRoles.FirstOrDefault();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new Claim("id", user.Id),

                }),
                Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration["Jwt:TokenLifeTime"])),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Response<AuthenticationToken>
            {
                Status = true,
                Message = "success",
                Data = new AuthenticationToken
                {
                    Token = tokenHandler.WriteToken(token)
                },
            };

        }
        #endregion

        #region ResetPassword
        public async Task<Response> ResetPassword(ResetPasswordCmd request)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                throw new BusinessLogicException("رکوردی با این مشخصات یافت نشد");
            }
            var userHashPassword = _userManager.PasswordHasher.HashPassword(user, request.NewPassword);
            user.PasswordHash = userHashPassword;
            return new Response
            {
                Status = true,
                Message = "success",

            };
        }
        #endregion


        #region UpsertUser
        public async Task<Response> UpsertUser(UpsertUserCmd request)
        {
            if (!string.IsNullOrEmpty(request.Id))
            {

                var user = await _userManager.FindByIdAsync(request.Id);
                if (user == null)
                {
                    throw new BusinessLogicException("خطای ناشناخته ای رخ داده است");
                }
                user = _mapper.Map(request, user);

                // update user role
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);
                await _userManager.AddToRolesAsync(user, request.Roles);

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    throw new Exception();
                }



            }
            else
            {
                var user = new ApplicationUser();

                var existingUser = await _userManager.FindByNameAsync(request.UserName);
                if (existingUser != null)
                {
                    throw new BusinessLogicException("این نام کاربری قبلن استفاده شده است. نام کاربری دیگری را امتحان کنید");
                }
                // user = await result.ProjectTo<ApplicationUser>(_mapper.ConfigurationProvider).First();
                user = _mapper.Map<ApplicationUser>(request);
                // user = _mapper.Map(request, user);
                user.Id = Guid.NewGuid().ToString();
                //  user.UserName = request.UserName;
                // user.PasswordHash = request.Password;
                var res = await _userManager.CreateAsync(user, request.Password);

                if (!res.Succeeded)
                {
                    throw new BusinessLogicException("خطای ناشناخته");
                }
                await _userManager.AddToRolesAsync(user, request.Roles);
            }

            return new Response
            {
                Status = true,
                Message = "success"

            };


        }
        #endregion

    }
}
