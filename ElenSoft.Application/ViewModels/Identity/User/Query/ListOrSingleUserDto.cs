using ElenSoft.Application.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.ViewModels.Identity.User.Query
{

    public class UsersDto : Pagenated
    {
        public ICollection<UserDto> Dtos { get; set; }

    }

    public class UserDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }


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

        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("departmentId")]
        public string DepartmentId { get; set; }
    }
}
