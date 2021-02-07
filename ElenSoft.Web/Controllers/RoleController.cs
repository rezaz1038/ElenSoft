using ElenSoft.Application.Repository.V1.IService;
using ElenSoft.Application.ViewModels;
using ElenSoft.Application.ViewModels.Identity.Role.Cmd;
using ElenSoft.Application.ViewModels.Identity.Role.Query;
using ElenSoft.Insfrastrcture;
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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        #region list

        [HttpGet]
        [Route(MapRoutes.Role.List)]
        public async Task<IActionResult> ListRoles([FromQuery] RolesQuery request)
        {
            try
            {
                var result = await _service.GetRoles(request);
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

        #region upsert
        [HttpPost]
        [Route(MapRoutes.Role.Upsert)]
        public async Task<IActionResult> UpsertRole([FromBody] UpsertRoleCmd request)
        {
            try
            {
                var result = await _service.UpsertRole(request);
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
                    //e.Message 
                });
            }
        }
        #endregion

        #region delete
        [HttpDelete(MapRoutes.Role.Delete)]
        public async Task<ActionResult> DeleteRole([FromRoute] string request)
        {
            try
            {
                var result = await _service.DeleteRole(request);
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
        [Route(MapRoutes.Role.Single)]
        public async Task<IActionResult> SingleRole([FromRoute] string request)
        {
            try
            {
                var result = await _service.GetRole(request);
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
