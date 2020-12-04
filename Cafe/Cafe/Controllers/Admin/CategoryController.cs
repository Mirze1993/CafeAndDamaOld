
using System.Linq;
using Cafe.Repostory;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Newtonsoft.Json;

namespace Cafe.Controllers.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class CategoryController : Controller
    {
        CategoryRepository repository;
        public CategoryController()
        {
            repository = new CategoryRepository();
        }

        public IActionResult AllCategory()
        {
            return View( repository.GetAll());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View("AddOrUpdate");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var list = new SubCategoryRepository().GetUISubCategories();
            
            ViewBag.SubCategorys = list;

            return View("AddOrUpdate",repository.GetByColumName("Id",id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult AddOrUpdate(Category category)
        {
            bool b;
            if (category.Id > 0) b =  repository.Update(category, category.Id);
            else b =  repository.Insert(category)>0;
            if (b)
                return RedirectToAction("AllCategory");
            return View("AddOrUpdate");
        }

        [HttpPost]
        public bool ChangeSubCategory(int id,int categoryId,bool value)
        {
            var subCategory= new SubCategoryRepository();
            var s = subCategory.GetByColumName("Id",id).FirstOrDefault();
            if (s == null) return false;
            if (value) s.CategoryId = categoryId;
            else s.CategoryId = 0;
            return subCategory.Update(s,id);
        }

       [HttpPost]
        public IActionResult Delete(int Id)
        {          
            var b=  repository.Delet(Id);           
            return RedirectToAction("AllCategory");
        }

       
    }
}