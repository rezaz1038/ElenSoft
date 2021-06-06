using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.ViewModels.Archive.Query
{
    public class ArchivesQuery : BasedFilter
    {
        //search base
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("categoryTitle")]
        public string CategoryTitle { get; set; }

        [JsonProperty("tagTitle")]
        public string TagTitle { get; set; }
    }
}
