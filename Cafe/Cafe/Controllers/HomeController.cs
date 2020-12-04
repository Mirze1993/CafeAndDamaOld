
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cafe.Repostory;
using Cafe.Tools;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.UIEntites;

namespace Cafe.Controllers
{
   
    public class HomeController : Controller
    {
        UserRepository repository;

        public HomeController( )
        {           
            repository = new UserRepository();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            
            if (User.Identity.IsAuthenticated)return RedirectToAction("Index");
            return View();
        }
      
        public IActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(UIAppUser model)
        {
            if (ModelState.IsValid)
            {
                var u = repository.GetByColumName("Username", model.UserName);
                    
                if (u.Count > 0) {
                    ModelState.AddModelError("", $"{model.UserName} is alreay  use");
                    return View();
                }
                var userId= repository.Insert(new AppUser()
                {
                    Email=model.Email,Name=model.Name,Phone=model.Phone,
                    UserName=model.UserName,Password=new HashCreate().CreateHashString(model.Password)
                });
                
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UILogin login)
        {
            if (ModelState.IsValid) { 
                var (b, u) = repository.ValidateUserCredentials(login.UserName, login.Password);
                if (b)
                {
                    var properties = new AuthenticationProperties
                    { 
                        IsPersistent = login.RememberMe,
                    };

                    var role = repository.GetUserRoles(u.Id);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,u.Id.ToString()),
                        new Claim(ClaimTypes.Name,u.UserName),
                    };
                    role.ForEach(x => claims.Add(new Claim(ClaimTypes.Role, x.RoleName)));

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    
                    
                    var principial = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principial,properties);
                    
                    return RedirectToAction("Index", "UserHome");
                }

                ModelState.AddModelError("", "Istifadeci adi ve ya parol sehhvdir");
                return View();
            }
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
                await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}