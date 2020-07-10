using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLCafe.ConcreateRepository;
using Cafe.UIModel;
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
            var list = await repository.GetAllAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View("AddOrUpdate");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var list = await subCategory.GetAllAsync();
            var UISubCats = new List<UISubCategory>();
            foreach (var item in list)
            {
                var u=JsonConvert.DeserializeObject<UISubCategory>(JsonConvert.SerializeObject(item));
                if (u.CategoryId == id) u.IsCheck = true;
                UISubCats.Add(u);
            }

            return View("AddOrUpdate", new UICategoryWithSub(
                (await repository.GetByIdAsync(id)).FirstOrDefault(),
                        UISubCats
                ));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            bool b;
            if (category.Id > 0) b = await repository.UpdateAsync(category, category.Id);
            else b = repository.Insert(category);
            if (b)
                return RedirectToAction("Index");
            return View("AddOrUpdate");
        }


    }
}