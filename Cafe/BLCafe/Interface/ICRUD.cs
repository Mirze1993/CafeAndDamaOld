using BLCafe.ConcreateRepository;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLCafe.Interface
{
    public interface ICRUD<T>:IExecuteCommand  where T : class, IEntity, new()
    {
        List<T>  GetById(int id);
        int Insert(T t);
        bool Update(T t, int id);
        bool Delet(int id);
        int RowCount();
        int RowCountWithSrc(string srcClm, string srcValue);
        List<T> getFromTo(int from, int to);
        List<T> getFromToWithSrc(int from, int to,string srcClm, string srcValue);
       

        Task<List<T>> GetAllAsync(params string[] column);
        Task<List<T>> GetByIdAsync(int id);
        Task<int> InsertAsync(T t);
        Task<bool> UpdateAsync(T t, int id);
        Task<bool> DeletAsync(int id);
        Task<List<T>> getFromToAsync(int from, int to);
        Task<List<T>> getFromToWithSrcAsync(int from, int to, string srcClm, string srcValue);
    }
}
