using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UIEntites
{
    public  class UISubCategoryListCategory
    {
        public SubCategory SubCategory { get; set; }
        public List<SelectListItem> Categores { get; set; }
        public UISubCategoryListCategory()
        {
            Categores = new List<SelectListItem>();
        }
    }
}
