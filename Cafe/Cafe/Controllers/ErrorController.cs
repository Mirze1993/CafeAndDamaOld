using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cafe.Controllers
{
    public class ErrorController:Controller
    {
        [Route("/Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var exceptionDetals = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            
           
            ViewBag.code = statusCode;
            return View();
        }

        [Route("/Error")]
        public IActionResult Error()
        {
            var exceptionDetals = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
           
            
            return View("HttpStatusCodeHandler");
        }
    }
}
