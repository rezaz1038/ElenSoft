using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.ViewModels.Identity.User
{
    public class LoginCmd
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }



        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
