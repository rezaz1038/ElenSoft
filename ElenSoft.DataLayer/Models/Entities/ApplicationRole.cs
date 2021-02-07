using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.DataLayer.Models.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUser> Users { get; set; }
    }
}
