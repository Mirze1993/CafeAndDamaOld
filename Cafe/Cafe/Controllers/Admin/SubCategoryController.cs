using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLCafe.ConcreateRepository;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Newtonsoft.Json;

namespace Cafe.Controllers.Admin
{
    public class SubCategoryController : Controller
    {
        SubCategoryRepository repository = new SubCategoryRepository();

        public  IActionResult AllSubCategory()
        {
            return View( repository.GetUISubCategories());
        }

        public IActionResult Add()
        {
            return View("AddOrUpdate");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var s = ( repository.GetUISubCategories(id)).FirstOrDefault();
            ViewBag.CategoryName = s.CategoryName;
            return View("AddOrUpdate", new SubCategory()
            {
                Id = s.Id,CategoryId=s.CategoryId,Description=s.Description,Name=s.Name
            });
        }

        [HttpPost]
        public IActionResult AddOrUpdate(SubCategory subCategory)
        {
            bool b;
            if (subCategory.Id > 0) b =  repository.Update(subCategory, subCategory.Id);
            else b =  repository.Insert(subCategory)>0;
            if (b)
                return RedirectToAction("AllSubCategory");
            return View("AddOrUpdate");
        }

        public bool SaveCategory(int catId, int id)
        {
            var sc = ( repository.GetById(id)).FirstOrDefault();
            sc.CategoryId = catId;
            return  repository.Update(sc, sc.Id);
        }

        [HttpPost]
        public  IActionResult Delete(int Id)
        {
            var b =  repository.Delet(Id);
            return RedirectToAction("AllSubCategory");
        }


        public string SubCatigoryForDropDown()
        {
            var s = repository.GetAll("Id", "Name");
            return JsonConvert.SerializeObject(s);
        }
        
    }
}