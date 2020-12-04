using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UIEntites
{
    public class UIProductSupCategory
    {
        public Product Product { get; set; }
        public List<SelectListItem> Subcategores { get; set; }
        public IFormFile Img { get; set; }
        public UIProductSupCategory()
        {
            Subcategores = new List<SelectListItem>();
        }
    }
}
