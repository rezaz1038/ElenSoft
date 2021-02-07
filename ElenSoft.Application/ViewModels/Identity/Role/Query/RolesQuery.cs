using Newtonsoft.Json;
using SoftIran.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.ViewModels.Identity.Role.Query
{
    public class RolesQuery : BasedFilter
    {


        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
