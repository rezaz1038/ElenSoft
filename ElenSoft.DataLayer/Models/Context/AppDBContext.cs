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
        //public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }


        //public DbSet<Department> Departments { get; set; }
        //public DbSet<Equipment> Equipment { get; set; }
        //public DbSet<EquipmentBrand> EquipmentBrands { get; set; }
        //public DbSet<EquipmentPlace> EquipmentPlaces { get; set; }
        //public DbSet<EquipmentType> EquipmentTypes { get; set; }
        //public DbSet<Offish> Offishes { get; set; }
        //public DbSet<OffishAction> OffishActions { get; set; }
        //public DbSet<OffishCategory> OffishCategories { get; set; }
        //public DbSet<OffishUser>   OffishUsers { get; set; }

        //public DbSet<OffishDescription> OffishDescriptions { get; set; }
        //public DbSet<OffishTemplate> OffishTemplates { get; set; }
        public DbSet<Category> Categories { get; set; }



    }
}
