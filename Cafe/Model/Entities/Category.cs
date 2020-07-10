using Model.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public sealed class Category:IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
