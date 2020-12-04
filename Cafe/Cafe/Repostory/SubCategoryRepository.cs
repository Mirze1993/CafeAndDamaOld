using MicroORM;
using Model.Entities;
using Model.UIEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Repostory
{
    public class SubCategoryRepository:CRUD<SubCategory>
    {
        public List<UISubCategory> GetUISubCategories(int id = 0)
        {
            string query = "SELECT  s.Id, s.Name, s.CategoryId, " +
                   "s.Description, c.Name as CategoryName " +
                   "FROM SubCategory  s LEFT JOIN Category c ON s.CategoryId = c.ID";

            using (CommanderBase commander = DBContext.CreateCommander())
            {
                if (id > 0) return commander.Reader<UISubCategory>(query + " WHERE s.Id=" + id);
                return commander.Reader<UISubCategory>(query);
            }

        }


    }
}
