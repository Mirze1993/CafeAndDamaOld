using Model.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLCafe.ConcreateRepository
{
    interface ISQLCommandExecute<T> where T : class
    {
        bool ExecuteQuery(string commandText, T t = null);


        object ExecuteScaller(string commandText, T t = null);


         List<T> ExecuteReader(string commandText, int id = -1);


         Task<bool> ExecuteQueryAsync(string commandText, T t = null);

         Task<object> ExecuteScallerAsync(string commandText, T t = null);

          Task<List<T>> ExecuteReaderAsync(string commandText, int id = -1);
        
    }
}
