using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.ViewModels.Tag.Query
{
    public class TagsQuery : BasedFilter
    {
        //search base
        [JsonProperty("tilte")]
        public string Title { get; set; }


    }
}
