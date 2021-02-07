using ElenSoft.Application.Repository.V1.IService;
using ElenSoft.Application.ViewModels;
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
        [Route("api/pgm/upsert")]
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
        [HttpDelete("api/pgm/{request}")]
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
        [Route("api/equipment/type/list ")]
        public async Task<IActionResult> ListPgms([FromQuery] CategoriesQuery request)
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
        [Route("api/pgm/{request}")]
        public async Task<IActionResult> SinglePgm([FromQuery] string request)
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



