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