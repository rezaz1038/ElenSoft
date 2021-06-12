using ElenSoft.Application.Repository.V1.IService;
using ElenSoft.Application.ViewModels;
using ElenSoft.Application.ViewModels.Archive.Cmd;
using ElenSoft.Application.ViewModels.Archive.Query;
using ElenSoft.Insfrastrcture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElenSoft.Web.Controllers
{

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class ArchiveController : ControllerBase
    {
        private readonly IArchive _service;
        public ArchiveController(IArchive service)
        {
            _service = service;
        }

        #region upsert
        [HttpPost]
        [Route(MapRoutes.Archive.Upsert)]
        public async Task<IActionResult> Upsert([FromBody] UpsertArchiveCmd request)
        {
            try
            {
                var result = await _service.UpsertArchive(request);
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
        #endregion

        #region delete
        [HttpDelete(MapRoutes.Archive.Delete)]
        public async Task<ActionResult> Delete([FromRoute] string request)
        {
            try
            {
                var result = await _service.DeleteArchive (request);
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

        #endregion

        #region list

        [HttpGet]
        [Route(MapRoutes.Archive.List)]
        public async Task<IActionResult> List ([FromQuery] ArchivesQuery request)
        {
            try
            {
                var result = await _service.GetArchives(request);
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
        #endregion


        #region get  single
        [HttpGet]
        [Route(MapRoutes.Archive.Single)]
        public async Task<IActionResult> Single ([FromRoute] string request)
        {
            try
            {
                var result = await _service.GetArchive(request);
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
        #endregion

    }
}
