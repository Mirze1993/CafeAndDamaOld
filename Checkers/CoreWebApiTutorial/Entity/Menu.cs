using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiTutorial.Entity
{
    public class Menu
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Menu()
        {

        }
        public Menu(int id,string name)
        {
            Id = id;Name = name;
        }
    }
}
