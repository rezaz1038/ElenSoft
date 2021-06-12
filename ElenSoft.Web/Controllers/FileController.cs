using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ElenSoft.Application.Profiles;
using ElenSoft.Application.Repository.V1.IService;
using ElenSoft.Application.ViewModels;
 
using ElenSoft.Insfrastrcture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.ValueGeneration.Internal;

namespace ElenSoft.Web.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("api/v1/file/upload"), DisableRequestSizeLimit]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Upload([FromForm] UploadFileCommand request)
        {


            //return Ok(await _fileService.Upload(request));

            try
            {
                var form = Request.Form;
                request.Title = form["title"];
                request.CategoryId = form["categoryId"];
                request.Files = form.Files.ToList();
                var result = await _fileService.Upload(request);
                return Ok(result);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(new Response
                {
                    Status = false,
                    Message = ex.Message
                });

            }
            catch (Exception e)
            {
                return BadRequest(new Response
                {
                    Status = false,
                    Message = ErrorMessages.UnkownError
                });
            }


        }




    }
}