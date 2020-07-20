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
    public class CategoryController : Controller
    {
        CategoryRepository repository = new CategoryRepository();
        SubCategoryRepository subCategory = new SubCategoryRepository();

        public async Task<IActionResult> AllCategory()
        {
            return View(await repository.GetAllAsync());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View("AddOrUpdate");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var list = await subCategory.GetUISubCategoriesAsync();
            //var UISubCats = new List<UISubCategory>();
            //foreach (var item in list)
            //{
            //    var u=JsonConvert.DeserializeObject<UISubCategory>(JsonConvert.SerializeObject(item));
            //    if (u.CategoryId == id) u.IsCheck = true;
            //    UISubCats.Add(u);
            //}
            ViewBag.SubCategorys = list;

            return View("AddOrUpdate", (await repository.GetByIdAsync(id)).FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(Category category)
        {
            bool b;
            if (category.Id > 0) b = await repository.UpdateAsync(category, category.Id);
            else b = await repository.InsertAsync(category);
            if (b)
                return RedirectToAction("AllCategory");
            return View("AddOrUpdate");
        }

        [HttpPost]
        public bool ChangeSubCategory(int id,int categoryId,bool value)
        {
            var s = subCategory.GetById(id).FirstOrDefault();
            if (s == null) return false;
            if (value) s.CategoryId = categoryId;
            else s.CategoryId = 0;
            return subCategory.Update(s,id);
        }

       [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {          
            var b= await repository.DeletAsync(Id);           
            return RedirectToAction("AllCategory");
        }

        public async Task<string> CatigoryForDropDown()
        {
            var list = await repository.GetAllAsync("Id","Name");
            return JsonConvert.SerializeObject(list);


        }
    }
}