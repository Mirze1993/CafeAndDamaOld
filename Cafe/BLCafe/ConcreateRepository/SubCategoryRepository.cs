using Model.Entities;
using Model.UIEntites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLCafe.ConcreateRepository
{
    public class SubCategoryRepository:CRUD<SubCategory>
    {
        string query = "SELECT  s.Id, s.Name, s.CategoryId, " +
                    "s.Description, c.Name as CategoryName " +
                    "FROM SubCategory  s LEFT JOIN Category c ON s.CategoryId = c.ID";
        public List<UISubCategory> GetUISubCategories(int id=0)
        {
            
                if (id > 0) return ExecuteReader<UISubCategory>(query + " WHERE s.Id="+id);
                return ExecuteReader<UISubCategory>(query);
               
            
        }

        public async Task<List<UISubCategory>> GetUISubCategoriesAsync(int id=0)
        {
           
                if (id > 0) return await ExecuteReaderAsync<UISubCategory>(query + " WHERE s.Id="+id);
                return await ExecuteReaderAsync<UISubCategory>(query);
            
        }
    }
}
