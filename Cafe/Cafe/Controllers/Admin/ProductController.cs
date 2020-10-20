using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLCafe.ConcreateRepository;
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

            return View();
        }

        public IActionResult UpdateProduct(int id)
        {
            var pr =  repository.GetUIProducts(id);
            var p = pr.FirstOrDefault();
            if (p == null) return RedirectToAction("AllProduct");

            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(UIProduct model)
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
             await repository.InsertAsync(new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                ImgPath = filename
            });
            return RedirectToAction("AllProduct");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UIProduct model)
        {

            string filename = model.ImgPath;

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
            var prd = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                ImgPath = filename
            };
            if (model.Id > 0)
            {
                prd.Id = model.Id;
                await repository.UpdateAsync(prd, prd.Id);
            }
            else await repository.InsertAsync(prd);

            return RedirectToAction("AllProduct");
        }

        public async Task<bool> SaveSubCategory(int catId, int id)
        {
            var pr = (await repository.GetByIdAsync(id)).FirstOrDefault();
            pr.SubCategoryId = catId;
            return await repository.UpdateAsync(pr, pr.Id);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id, string path)
        {
            var b = await repository.DeletAsync(id);
            var filePath = Path.Combine(Environment.WebRootPath, "userimg", path ?? "notfound");
            bool t = System.IO.File.Exists(filePath);
            if (b && t) System.IO.File.Delete(filePath);
            return RedirectToAction("AllProduct");
        }
    }
}