using Newtonsoft.Json;
using SoftIran.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.ViewModels.Identity.User.Query
{
    public class UsersQuery : BasedFilter
    {
        //search base on 


        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("roleName")]
        public string RoleName { get; set; }

        [JsonProperty("claim")]
        public string Claim { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }



        [JsonProperty("departmentId")]
        public string DepartmentId { get; set; }
    }
}
