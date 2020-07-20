using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.UIEntites
{
    public class UIProduct
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
       
        public string Description { get; set; }

        public string SubCategoryName { get; set; }

        public IFormFile Img { get; set; }

        public string ImgPath { get; set; }
    }
}
