using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ElenSoft.DataLayer.Models.Entities;

namespace ElenSoft.DataLayer.Models.Context
{
    public class AppDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>,
        IdentityUserRole<string>, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        //protected override void OnModelCreating(ModelBuilder  modelBuilder)
        //{
        //  // base.OnModelCreating(builder);
        //    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //}
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag>  Tags { get; set; }
        public DbSet<Archive> Archives { get; set; }

        public DbSet<FolderInfo> FolderInfos { get; set; }
        public DbSet<FolderSecurity> FolderSecurities { get; set; }


        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<EquipmentPlace> EquipmentPlaces { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceBrand> DeviceBrands { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
    }
}
