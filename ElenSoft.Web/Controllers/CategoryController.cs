using ElenSoft.Application.Repository.V1.IService;
using ElenSoft.Application.ViewModels;
using ElenSoft.Application.ViewModels.Archive.Query;
using ElenSoft.Application.ViewModels.Category.Cmd;
using ElenSoft.Application.ViewModels.Category.Query;
using ElenSoft.Insfrastrcture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElenSoft.Web.Controllers
{

    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategory _service;
        public CategoryController(ICategory service)
        {
            _service = service;
        }

        #region upsert
        [HttpPost]
        [Route(MapRoutes.Category.Upsert)]
        public async Task<IActionResult> Upsert([FromBody] UpsertCategoryCmd request)
        {
            try
            {
                var result = await _service.UpsertCategory(request);
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
        [HttpDelete(MapRoutes.Category.Delete)]  
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete([FromRoute] string request)
        {
            try
            {
                var result = await _service.DeleteCategory(request);
                return Ok(result);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(new Response
                {
                    Status = false,
                    Message =ex.Message
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
        [Route(MapRoutes.Category.List)]
        public async Task<IActionResult> List ([FromQuery] CategoriesQuery request)
        {
            try
            {
                var result = await _service.GetCategories(request);
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
        [Route(MapRoutes.Category.Single)]
        public async Task<IActionResult> Single ([FromRoute] string request)
        {
            try
            {
                var result = await _service.GetCategory(request);
                return Ok(result);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(new Response
                {
                    Status = false,
                    Message =ex.Message
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



