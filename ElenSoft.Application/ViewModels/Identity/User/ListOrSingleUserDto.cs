using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
 

namespace ElenSoft.Application.ViewModels.Identity.User
{

    public class UsersDto : Pagenated
    {
        public ICollection<UserDto> Dtos { get; set; }

    }

    public class UserDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("roles")]
        public IList<string> RoleDtos { get; set; }

        [JsonProperty("jobTitles")]
        public IList<Claim> JobTitles { get; set; }

    }
}
