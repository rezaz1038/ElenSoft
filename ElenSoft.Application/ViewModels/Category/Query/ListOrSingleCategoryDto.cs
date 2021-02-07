using ElenSoft.Application.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.ViewModels.Category.Query
{
    public class CategoriesDto : Pagenated
    {
        public ICollection<CategoryDto> Dtos { get; set; }
    }


    public class CategoryDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

 
    }
}



