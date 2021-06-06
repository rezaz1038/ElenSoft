using ElenSoft.Application.Profiles;
using ElenSoft.Application.Repository.V1.IService;
using ElenSoft.Application.Repository.V1.Services;
using ElenSoft.DataLayer.Models.Context;
using ElenSoft.DataLayer.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {

            #region Database Context
            services.AddDbContext<AppDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DeflautString"));

            });
            #endregion

            #region identity

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.User.RequireUniqueEmail = true;
                //  options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<AppDBContext>()
                          .AddDefaultTokenProviders();
            #endregion


            #region IReopsitory Service
            //services.AddScoped<IJWTTokenGenerator, JWTTokenGenerator>();
            services.AddTransient<ICategory, CategoryService>();
            services.AddTransient<ITag, TagService>();
            services.AddTransient<IArchive, ArchiveService>();
            services.AddTransient<IFileService, FileService>();
             services.AddTransient<IUserService, UserService>();
             services.AddTransient<IRoleService, RoleService>();


            #endregion


            #region Automapper
             services.AddAutoMapper(typeof(ArchiveProfile));
             services.AddAutoMapper(typeof(IdentityProfile));
            #endregion

            #region Autentication
            var TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                // ValidAudience = "",
                // ValidIssuer = "",
                RequireExpirationTime = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                ValidateIssuerSigningKey = true
            };

            services.AddSingleton(TokenValidationParameters);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>

            options.TokenValidationParameters = TokenValidationParameters
            );

            #endregion


            #region Authorization
            services.AddAuthorization();
            #endregion

            #region Api Versioning
            //// Add API Versioning to the Project
            //services.AddApiVersioning(config =>
            //{
            //    // Specify the default API Version as 1.0
            //    config.DefaultApiVersion = new ApiVersion(1, 0);
            //    // If the client hasn't specified the API version in the request, use the default API version number 
            //    config.AssumeDefaultVersionWhenUnspecified = true;
            //    // Advertise the API versions supported for the particular endpoint
            //    config.ReportApiVersions = true;
            //    //HTTP Header based versioning
            //    config.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            //});
            #endregion


            services.AddSwaggerGen();

            services.AddMvc(
                option => option.EnableEndpointRouting = false
                );
            services.AddControllers();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(x =>
                {
                    x.AllowAnyHeader();
                    x.AllowAnyOrigin();
                    x.AllowAnyMethod();
                });
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting();
            app.UseMvc();
        }
    }
}
