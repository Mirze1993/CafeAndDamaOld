using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Controllers.Admin
{
    public class UserController : Controller
    {
        public IActionResult AllUser()
        {
            return View();
        }
    }
}