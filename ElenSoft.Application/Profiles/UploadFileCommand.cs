using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ElenSoft.Application.Profiles
{
    public class UploadFileCommand: UploadFile
    {
        public ICollection<IFormFile> Files { get; set; }
    }

    public class UploadFile
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public ICollection<string> Tags { get; set; }
    }
}