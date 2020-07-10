using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.UIModel
{
    public class UICategoryWithSub
    {
        public Category category { get; set; }
        public List<UISubCategory> subCategories { get; set; }
        public UICategoryWithSub()
        {
            subCategories = new List<UISubCategory>();
        }

        public UICategoryWithSub(Category _category, List<UISubCategory> _subCategories)
        {
            category = _category;
            subCategories = _subCategories;
        }
    }
}
