using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.ViewModels.Identity.User
{
    public class UsersQuery : BasedFilter
    {
        //search base on 


        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }


        [JsonProperty("userName")]
        public string UserName { get; set; }

    }
}
