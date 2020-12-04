using System;

using System.Text;

namespace Model.UIEntites
{
    public class UISubCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}
