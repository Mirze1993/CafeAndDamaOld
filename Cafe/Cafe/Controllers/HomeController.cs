using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLCafe.ConcreateRepository;
using BLCafe.Interface;
using Cafe.Tools;
using Cafe.Tools.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.UIEntites;

namespace Cafe.Controllers
{
    public class HomeController : Controller
    {
        IUserRepository repository;

        IUserService userService;
        public HomeController(IUserService service,IUserRepository userRepository)
        {
            userService = service;
            repository = userRepository;
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
                var u= repository.Reader<AppUser>($"Select Id  from AppUser Where Username='{model.UserName}'");
                if (u.Count > 0) {
                    ModelState.AddModelError("", $"{model.UserName} is alreay  use");
                    return View();
                }
                var userId= repository.Insert(new Model.Entities.AppUser()
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
                var (b, u) = await userService.ValidateUserCredentialsAsync(login.UserName, login.Password);
                if (b)
                {
                    var properties = new AuthenticationProperties
                    { 
                        IsPersistent = login.RememberMe
                    };
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,u.Id.ToString()),
                        new Claim(ClaimTypes.Name,u.UserName),
                    };

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