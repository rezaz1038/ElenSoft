using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.ViewModels.Tag.Query
{
    public class TagsDto : Pagenated
    {
        public ICollection<TagDto> Dtos { get; set; }
    }


    public class TagDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

    }
}



