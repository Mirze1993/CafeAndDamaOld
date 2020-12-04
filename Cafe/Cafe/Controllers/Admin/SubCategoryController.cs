
using System.Linq;
using Cafe.Repostory;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.UIEntites;
using Newtonsoft.Json;

namespace Cafe.Controllers.Admin
{
    public class SubCategoryController : Controller
    {
        SubCategoryRepository repository = new SubCategoryRepository();

        public IActionResult AllSubCategory()
        {
            return View(repository.GetUISubCategories());
        }

        public IActionResult Add()
        {
            var model = new UISubCategoryListCategory();            
            var c = new CategoryRepository().GetAll("Id", "Name");
            if (c.Count > 0)
                foreach (var item in c)
                {
                    model.Categores.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(item.Name, item.Id.ToString()));
                }

            return View("AddOrUpdate", model);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var model = new UISubCategoryListCategory();
            model.SubCategory = repository.GetByColumName("Id", id).FirstOrDefault();
            var c = new CategoryRepository().GetAll("Id", "Name");
            if (c.Count > 0)
                foreach (var item in c)
                {
                    model.Categores.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(item.Name, item.Id.ToString()));
                }

            return View("AddOrUpdate", model);
        }

        [HttpPost]
        public IActionResult AddOrUpdate(UISubCategoryListCategory model)
        {
            bool b;
            if (model.SubCategory.Id > 0) b = repository.Update(model.SubCategory, model.SubCategory.Id);
            else b = repository.Insert(model.SubCategory) > 0;
            if (b)
                return RedirectToAction("AllSubCategory");
            return View("AddOrUpdate");
        }

      

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var b = repository.Delet(Id);
            return RedirectToAction("AllSubCategory");
        }



    }
}