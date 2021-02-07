using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.ViewModels.Identity.User.Cmd
{
    public class ChangePasswordCmd
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string CurrentPassword { get; set; }

        [JsonProperty("password")]
        public string NewPassword { get; set; }
    }
}
