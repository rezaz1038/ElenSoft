using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.ViewModels.Tag.Cmd
{
    public class UpsertTagCmd
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tilte")]
        public string Title { get; set; }


    }
}
