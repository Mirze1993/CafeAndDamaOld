using Model.Entities;
using Model.UIEntites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLCafe.ConcreateRepository
{
    public class ProductRepository : CRUD<Product>
    {
        string query = "SELECT  p.Id, p.Name, p.Price, p.SubCategoryId, " +
                   "p.Description, p.ImgPath , s.Name as SubCategoryName " +
                   "FROM Product p LEFT JOIN SubCategory s ON p.SubCategoryId = s.Id";



        public List<UIProduct> GetUIProducts(int id = 0)
        {
            if (id > 0) return ExecuteReader<UIProduct>(query + " WHERE p.Id=" + id);
            return ExecuteReader<UIProduct>(query);
        }

        public async Task<List<UIProduct>> GetUIProductAsync(int id = 0)
        {

            if (id > 0) return await ExecuteReaderAsync<UIProduct>(query + " WHERE p.Id=" + id);
            return await ExecuteReaderAsync<UIProduct>(query);

        }
    }
}
