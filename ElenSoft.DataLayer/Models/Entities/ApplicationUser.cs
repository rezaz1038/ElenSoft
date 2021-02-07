using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.DataLayer.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Code { get; set; }
        public string Avatar { get; set; }
       
        

        public ICollection<ApplicationRole> Roles { get; set; }
        
    }
}
