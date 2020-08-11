using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLCafe.ConcreateRepository;
using Cafe.Tools;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Model.UIEntites;

namespace Cafe.Controllers
{
    public class HomeController : Controller
    {
        UserRepository repository = new UserRepository();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
      
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(UIAppUser model)
        {
            if (ModelState.IsValid)
            {
                var u= repository.ExecuteReader($"Select Id  from AppUser Where Username='{model.UserName}'");
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

        
        //public async Task<IActionResult> Login(UILogin login)
        //{
        //    await HttpContext.SignInAsync()
        //}

    }
}