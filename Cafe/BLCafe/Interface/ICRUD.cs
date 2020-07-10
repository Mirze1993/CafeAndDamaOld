using Model.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLCafe.Interface
{
    interface ICRUD<T> where T : IEntity, new()
    {
        List<T> GetAll(params string[] column);
        List<T> GetById(int id);
        bool Insert(T t);
        bool Update(T t, int id);
        bool Delet(int id);

        Task<List<T>> GetAllAsync(params string[] column);
        Task<List<T>> GetByIdAsync(int id);
        Task<bool> InsertAsync(T t);
        Task<bool> UpdateAsync(T t, int id);
        Task<bool> DeletAsync(int id);
    }
}
