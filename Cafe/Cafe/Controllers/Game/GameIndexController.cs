using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Controllers.Game
{
    public class GameIndexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}