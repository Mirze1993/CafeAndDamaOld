using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebApiTutorial.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApiTutorial.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
       [HttpGet]
       public async Task<ActionResult> GetMenus()
        {
            await Task.Delay(3000);
            return  Ok(new Menu(1,"mirze"));
        } 
    }
}