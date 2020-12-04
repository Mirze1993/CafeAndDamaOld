using System;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Controllers.AdminControllers
{
    [Authorize]
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult AdminIndex()
        {
            return View();
        }


    }
}