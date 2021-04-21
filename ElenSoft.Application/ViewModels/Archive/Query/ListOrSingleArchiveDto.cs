using ElenSoft.Application.ViewModels.Category.Query;
using ElenSoft.Application.ViewModels.Tag.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.ViewModels.Archive.Query
{
    public class ArchivesDto : Pagenated
    {
        public ICollection<ArchiveDto> Dtos { get; set; }
    }


    public class ArchiveDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public object Name { get; set; }

        [JsonProperty("descriptiion")]
        public string Description { get; set; }

        [JsonProperty("filesize")]
        public string Filesize { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("tag")]
        public TagDto Tag { get; set; }

        [JsonProperty("category")]
        public CategoryDto Category { get; set; }

    }
}



