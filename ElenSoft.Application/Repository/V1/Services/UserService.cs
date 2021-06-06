

using AutoMapper;
using AutoMapper.QueryableExtensions;
using ElenSoft.Application.Repository.V1.IService;
using ElenSoft.Application.ViewModels;
using ElenSoft.Application.ViewModels.Identity.User;
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
using System.IO;
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
        //private readonly IJWTTokenGenerator _jwtTokenGenerator;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly HttpContext _httpContext;
        private readonly AppDBContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager,
                           SignInManager<ApplicationUser> signInManager,
                         //  IJWTTokenGenerator jWTTokenGenerator,
                                IHttpContextAccessor httpContextAccessor,
                                RoleManager<ApplicationRole> roleManager,
                                IConfiguration configuration,
                               AppDBContext context, IMapper mapper)
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _httpContext = httpContextAccessor.HttpContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }



        #region delete user
        public async Task<Response> DeleteUser(string id)
        {
            var user = await _userManager.Users.FirstAsync(z => z.Id == id);
            if (user == null)
            {
                throw new("رکوردی با این مشخصات یافت نشد");
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
                throw new("رکوردی با این مشخصات یافت نشد");
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


        #region List Users
        public async Task<Response<UsersDto>> GetUsers(UsersQuery request)
        {

            var result = _userManager.Users
                .AsQueryable();

            ///pagenating
            int take = request.PageSize;
            int skip = (request.PageId - 1) * take;

            int totalPages = (int)Math.Ceiling(result.Count() / (double)take);

            var finalResult = result.OrderBy(x => x.UserName).Skip(skip).Take(take).AsQueryable();
            //----------------

            var resultData = new UsersDto
            {
                Dtos = await result.Select(u => new UserDto()
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,

                    RoleDtos = _userManager.GetRolesAsync(u).Result,
                    JobTitles = _userManager.GetClaimsAsync(u).Result
                }).ToListAsync(),
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

        #region UpsertUser
        public async Task<Response> UpsertUser(UpsertUserCmd request)
        {



            if (!string.IsNullOrEmpty(request.Id))
            {
                var user = await _userManager.FindByIdAsync(request.Id);
                if (user == null)
                {
                    throw new("کاربری برای ویرایش موجود نمی باشد");
                }
                user = _mapper.Map(request, user);
                // user.Department = department;

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
                    throw new("این نام کاربری استفاده شده است. نام کاربری دیگری را امتحان کنید");
                }
                // user = await result.ProjectTo<ApplicationUser>(_mapper.ConfigurationProvider).First();
                //user = _mapper.Map<ApplicationUser>(request);
                //  user = _mapper.Map(request, user);
                user.FirstName = request.FirstName;
                user.UserName = request.UserName;
                user.LastName = request.LastName;
                user.Id = Guid.NewGuid().ToString();



                var res = await _userManager.CreateAsync(user, request.Password);

                if (!res.Succeeded)
                {
                    throw new("خطای  ناشناخته");
                }

            }

            return new Response
            {
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
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                throw new BusinessLogicException(" کلمه عبور را درست وارد نکرده اید");

            }


            IList<Claim> claims = await _userManager.GetClaimsAsync(user);

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName));
            claims.Add(new Claim("id", user.Id));

            ////authentucation
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration["Jwt:TokenLifeTime"])),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            /////

            return new Response<AuthenticationToken>
            {
                Status = true,
                Message = "success",
                Data = new AuthenticationToken
                {
                    Token = tokenHandler.WriteToken(token),

                    // FirstName = user.FirstName,
                    // LastName = user.LastName,
                    //  FullName = $"{user.FirstName} {user.LastName}",

                    //   UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    // result= result
                    //    Avatar = user.Avatar,


                    //Roles = userRoles,
                    //Claims = userClaims.Select(c => c.Type).ToList()
                },
            };

        }
        #endregion


        #region register
        public async Task<Response> RegisterUser(RegisterCmd request)
        {
            var user = new ApplicationUser();

            var existingUser = await _userManager.FindByNameAsync(request.UserName);
            if (existingUser != null)
            {
                throw new BusinessLogicException("این نام کاربری استفاده شده است. نام کاربری دیگری را امتحان کنید");

            }

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail != null)
            {
                throw new BusinessLogicException("این ایمیل استفاده شده است. نام کاربری دیگری را امتحان کنید");

            }

            user.UserName = request.UserName;
            user.Email = request.Email;

            user.Id = Guid.NewGuid().ToString();


            var res = await _userManager.CreateAsync(user, request.Password);


            if (!res.Succeeded)
            {
                throw new BusinessLogicException("خطای پسورد ");
            }
            else
            {
                //Add role to user
                if (!await _roleManager.RoleExistsAsync("guest"))
                {
                    await _roleManager.CreateAsync(new ApplicationRole()
                    {
                        Name = "guest"
                    });
                }
                await _userManager.AddToRoleAsync(user, "guest");

                //Add claim to user
                var claim = new Claim("JobTitle", "user");

                await _userManager.AddClaimAsync(user, claim);

            }


            return new Response
            {
                Status = true,
                Message = "success"

            };

        }

        #endregion


        #region  levelUp
        public async Task<Response> LevelUpUser(LevelUpCmd request)
        {


            ////


            if (!string.IsNullOrEmpty(request.Id))
            {
                var user = await _userManager.FindByIdAsync(request.Id);
                if (user == null)
                {
                    throw new BusinessLogicException("کاربری برای ویرایش موجود نمی باشد");
                }
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.UserName = request.UserName;
                user.Email = request.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {

                    //edit roels
                    var currentRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    foreach (var roleItem in request.Roles)
                    {
                        if (!await _roleManager.RoleExistsAsync(roleItem))
                        {
                            await _roleManager.CreateAsync(new ApplicationRole()
                            {
                                Name = roleItem
                            });
                        }
                        await _userManager.AddToRoleAsync(user, roleItem);
                    }
                    //edit claims
                    IList<Claim> claims = await _userManager.GetClaimsAsync(user);
                    await _userManager.RemoveClaimsAsync(user, claims);

                    foreach (var claimItem in request.JobTitles)
                    {

                        claims.Add(new Claim("JobTitle", claimItem));
                    }
                    await _userManager.AddClaimsAsync(user, claims);

                    ///

                }




                //if (!result.Succeeded)
                //{
                //    throw new Exception();
                //}

                // result = await _userManager.CreateAsync(user, request.Password);

                //if (!result.Succeeded)
                //{
                //  throw new("خطای پسورد ");
                //}

                if (user == null)
                {
                    /// var userfromDb = await _userManager.FindByNameAsync(user.UserName);
                    // var claim = new Claim("JobTitle", request.JobTitle);
                    // await _userManager.AddToRoleAsync(user, request.Role);
                    // await _userManager.AddClaimAsync(user, claim);
                    // result = await _userManager.UpdateAsync(user);
                }

                ///




                //    var user = new ApplicationUser();

                //var existingUser = await _userManager.FindByNameAsync(request.UserName);
                //if (existingUser != null)
                //{
                //    throw new BusinessLogicException("این نام کاربری استفاده شده است. نام کاربری دیگری را امتحان کنید");

                //}

                //var existingEmail = await _userManager.FindByEmailAsync(request.Email);
                //if (existingEmail != null)
                //{
                //    throw new BusinessLogicException("این ایمیل استفاده شده است. نام کاربری دیگری را امتحان کنید");

                //}

                //if  (!(await _roleManager.RoleExistsAsync(request.Role)))
                //{
                //    await _roleManager.CreateAsync(new ApplicationRole()
                //    {
                //        Name = request.Role
                //    } ); 
                //}
                //// user = await result.ProjectTo<ApplicationUser>(_mapper.ConfigurationProvider).First();
                ////user = _mapper.Map<ApplicationUser>(request);
                ////  user = _mapper.Map(request, user);
                //user.UserName = request.UserName;
                //user.Email = request.Email;

                //user.Id = Guid.NewGuid().ToString();



                //var res = await _userManager.CreateAsync(user, request.Password);

                //if (!res.Succeeded)
                //{
                //    throw new("خطای پسورد ");
                //}

                //if (res.Succeeded)
                //{
                //    var userfromDb = await _userManager.FindByNameAsync(user.UserName);
                //    var claim = new Claim("JobTitle", request.JobTitle);
                //    await _userManager.AddToRoleAsync(userfromDb, request.Role);
                //    await _userManager.AddClaimAsync(userfromDb, claim);

                //} 
            }
            else
            {
                throw new BusinessLogicException("این ا ");

            }

            return new Response
            {
                Status = true,
                Message = "success"

            };

        }


        #endregion
        #region  GetClaims
        public async Task<Response<ClaimsDto>> GetClaims()
        {
            List<string> JobList = new List<string>();

            JobList.Add("user");
            JobList.Add("admin");
            JobList.Add("adminplus");
            JobList.Add("young");
            JobList.Add("kids");
            JobList.Add("old");
            JobList.Add("male");
            JobList.Add("female");

            var resultData = new ClaimsDto()
            {
                JobTitle = JobList

            };
            return new Response<ClaimsDto>
            {
                Data = resultData,
                Status = true,
                Message = "success"

            };
        }
        #endregion
        //#region ResetPassword
        //public async Task<Response> ResetPassword(ResetPasswordCmd request)
        //{
        //    var user = await _userManager.FindByIdAsync(request.Id);
        //    if (user == null)
        //    {
        //        throw new BusinessLogicException("رکوردی با این مشخصات یافت نشد");
        //    }
        //    var userHashPassword = _userManager.PasswordHasher.HashPassword(user, request.NewPassword);
        //    user.PasswordHash = userHashPassword;

        //    var result = await _userManager.UpdateAsync(user);
        //    if (!result.Succeeded)
        //    {
        //        throw new Exception();
        //    }

        //    return new Response
        //    {
        //        Status = true,
        //        Message = "success",

        //    };
        //}
        //#endregion

        //#region ChangePassword
        //public async Task<Response> ChangePassword(ChangePasswordCmd request)
        //{
        //    var userId = _httpContext.GetUserId();
        //    var user = _context.Users.FirstOrDefault(x => x.Id == userId);

        //    var userHasValidPassword = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
        //    if (!userHasValidPassword)
        //    {
        //        throw new BusinessLogicException("رمز عبور صحیح نمی باشد");
        //    }

        //    var hashPassword = _userManager.PasswordHasher.HashPassword(user, request.NewPassword);
        //    user.PasswordHash = hashPassword;
        //    await _userManager.UpdateAsync(user);

        //    return new Response
        //    {
        //        Status = true,
        //        Message = "success"

        //    };
        //}

        //#endregion


    }
}
