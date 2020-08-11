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

        public async Task<IActionResult> AllSubCategory()
        {
            return View(await repository.GetUISubCategoriesAsync());
        }

        public IActionResult Add()
        {
            return View("AddOrUpdate");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var s = (await repository.GetUISubCategoriesAsync(id)).FirstOrDefault();
            ViewBag.CategoryName = s.CategoryName;
            return View("AddOrUpdate", new SubCategory()
            {
                Id = s.Id,CategoryId=s.CategoryId,Description=s.Description,Name=s.Name
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(SubCategory subCategory)
        {
            bool b;
            if (subCategory.Id > 0) b = await repository.UpdateAsync(subCategory, subCategory.Id);
            else b = await repository.InsertAsync(subCategory)>0;
            if (b)
                return RedirectToAction("AllSubCategory");
            return View("AddOrUpdate");
        }

        public async Task<bool> SaveCategory(int catId, int id)
        {
            var sc = (await repository.GetByIdAsync(id)).FirstOrDefault();
            sc.CategoryId = catId;
            return await repository.UpdateAsync(sc, sc.Id);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            var b = await repository.DeletAsync(Id);
            return RedirectToAction("AllSubCategory");
        }


        public async Task<string> SubCatigoryForDropDown()
        {
            var s =await repository.GetAllAsync("Id", "Name");
            return JsonConvert.SerializeObject(s);
        }
        
    }
}