using System;

using System.IO;
using System.Linq;
using Cafe.Repostory;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.UIEntites;

namespace Cafe.Controllers.Admin
{
    public class ProductController : Controller
    {
        public IHostingEnvironment Environment { get; }
        public ProductController(IHostingEnvironment environment)
        {
            Environment = environment;
        }

        ProductRepository repository = new ProductRepository();

        public IActionResult AllProduct()
        {
            return View(repository.GetUIProducts());
        }

        public IActionResult AddProduct()
        {
            var model = new UIProductSupCategory(); 
            var c = new SubCategoryRepository().GetAll("Id", "Name");
            if (c.Count > 0)
                foreach (var item in c)
                {
                    model.Subcategores.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(item.Name, item.Id.ToString()));
                }

            return View(model);
            
        }

        public IActionResult UpdateProduct(int id)
        {
            var pr =  repository.GetByColumName("Id",id);            
            var model = new UIProductSupCategory();model.Product= pr.FirstOrDefault();
            var c = new SubCategoryRepository().GetAll("Id", "Name");
            if (c.Count > 0)
                foreach (var item in c)
                {
                    model.Subcategores.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(item.Name, item.Id.ToString()));
                }

            return View(model);
        }

        [HttpPost]
        public  IActionResult AddProduct(UIProductSupCategory model)
        {

            string filename = null;

            if (model.Img != null)
            {
                filename = Guid.NewGuid().ToString() + "_"
                    + model.Img.FileName.Split("\\").LastOrDefault();
                string fn = Path.Combine(Environment.WebRootPath, "userimg", filename);
                using (FileStream fs = new FileStream(fn, FileMode.Create))

                {
                    model.Img.CopyTo(fs);
                }
            }
              repository.Insert(model.Product);
            return RedirectToAction("AllProduct");
        }

        [HttpPost]
        public  IActionResult UpdateProduct(UIProductSupCategory model)
        {

            string filename = model.Product.ImgPath;

            if (model.Img != null)
            {
                if (filename == null)
                    filename = Guid.NewGuid().ToString() + "_"
                    + model.Img.FileName.Split("\\").LastOrDefault();
               
                string fn = Path.Combine(Environment.WebRootPath, "userimg", filename);
                using (FileStream fs = new FileStream(fn, FileMode.Create))
                {
                    model.Img.CopyTo(fs);
                }
            }
            
            repository.Update(model.Product, model.Product.Id);
            return RedirectToAction("AllProduct");
        }

        public bool SaveSubCategory(int catId, int id)
        {
            var pr = repository.GetByColumName("Id",id).FirstOrDefault();
            pr.SubCategoryId = catId;
            return  repository.Update(pr, pr.Id);
        }

        [HttpPost]
        public ActionResult Delete(int id, string path)
        {
            var b =  repository.Delet(id);
            var filePath = Path.Combine(Environment.WebRootPath, "userimg", path ?? "notfound");
            bool t = System.IO.File.Exists(filePath);
            if (b && t) System.IO.File.Delete(filePath);
            return RedirectToAction("AllProduct");
        }
    }
}