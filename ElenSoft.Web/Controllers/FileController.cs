using System.Linq;
using System.Threading.Tasks;
using ElenSoft.Application.Profiles;
using ElenSoft.Application.Repository.V1.IService;
using ElenSoft.DataLayer.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.ValueGeneration.Internal;

namespace ElenSoft.Web.Controllers
{
    public class FileController: Controller
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("api/v1/file/upload")]
        public async Task<IActionResult> Upload([FromForm] UploadFileCommand request)
        {
            var form = Request.Form;
            request.Title = form["title"];
            request.CategoryId = form["categoryId"];
            request.Files = form.Files.ToList();

            return Ok(await _fileService.Upload(request));
        }
    }
}