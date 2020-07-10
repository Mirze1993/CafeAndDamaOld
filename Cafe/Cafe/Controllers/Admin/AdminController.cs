using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Controllers.AdminControllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult AdminIndex()
        {
            return View();
        }


    }
}