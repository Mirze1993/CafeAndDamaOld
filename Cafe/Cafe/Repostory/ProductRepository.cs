using MicroORM;
using Model.Entities;
using Model.UIEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Repostory
{
    public class ProductRepository:CRUD<Product>
    {

        public List<UIProduct> GetUIProducts(int id = 0)
        {
            string query = "SELECT  p.Id, p.Name, p.Price, p.SubCategoryId, " +
                  "p.Description, p.ImgPath , s.Name as SubCategoryName " +
                  "FROM Product p LEFT JOIN SubCategory s ON p.SubCategoryId = s.Id";
            using (CommanderBase commander = DBContext.CreateCommander())
            {
                if (id > 0) return commander.Reader<UIProduct>(query + " WHERE p.Id=" + id);
                return commander.Reader<UIProduct>(query);
            }
        }
    }
}
