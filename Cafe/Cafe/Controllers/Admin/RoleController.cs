using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe.Repostory;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;

namespace Cafe.Controllers.Admin
{
    public class RoleController : Controller
    {
        RoleRepository repository = new RoleRepository();

        public IActionResult AllRole()
        {
            return View( repository.GetAll());
        }

        public IActionResult Add()
        {
            return View("AddOrUpdate");
        }

        public IActionResult Update(int id)
        {
            return View("AddOrUpdate", repository.GetByColumName("Id",id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult AddOrUpdate(Role model)
        {
            if (model.Id > 0)
            {
                var m = repository.GetByColumName("Id", model.Id).FirstOrDefault();
                if (m == null) return RedirectToAction("AllRole");
                repository.Update(model, model.Id);
            }
            else
            {
                repository.Insert(model);
            }
            return RedirectToAction("AllRole");
        }
    }
}