using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebApiTutorial.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApiTutorial.Controller.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        /// <summary>
        /// get All
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Todo
        ///     {
        ///        
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>GET All</returns>
        /// <response code="200">ok</response>                
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]        
       public async Task<ActionResult<Menu>> GetMenus()
        {
            await Task.Delay(3000);
           return Ok(new Menu(1, "mirze")); 
        } 
    }
}