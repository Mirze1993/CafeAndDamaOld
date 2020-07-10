using Model.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Product:IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public string ImgPath { get; set; }
    }
}
