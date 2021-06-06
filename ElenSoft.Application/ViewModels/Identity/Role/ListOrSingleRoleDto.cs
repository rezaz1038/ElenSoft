using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.ViewModels.Identity.Role
{
    public class RolesDto : Pagenated
    {
        public ICollection<RoleDto> Dtos { get; set; }
    }

    public class RolesDtoWithoutPagenated
    {
        public ICollection<RoleDto> Dtos { get; set; }
    }

    public class RoleDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }


        [JsonProperty("Name")]
        public string Name { get; set; }

    }
}

