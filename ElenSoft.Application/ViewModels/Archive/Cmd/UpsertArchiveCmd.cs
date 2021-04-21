using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.ViewModels.Archive.Cmd
{
   public class UpsertArchiveCmd
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("descriptiion")]
        public object Descriptiion { get; set; }

        [JsonProperty("filesize")]
        public string Filesize { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("tageId")]
        public string TageId { get; set; }

        [JsonProperty("categoryId")]
        public string CategoryId { get; set; }


     
    }
}
