using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BLCafe.Interface
{
    public interface IExecuteCommand
    {
        bool ExecuteQuery<T>(string commandText, T t) where T : class, IEntity, new();

         Task<bool> ExecuteQueryAsync<T>(string commandText, T t ) where T : class, IEntity, new();

        Task<bool> ExecuteQueryAsyncc(string commandText, List<SqlParameter> parameters);
        
        object ExecuteScaller<T>(string commandText, T t) where T : class, IEntity, new();

        object ExecuteScaler(string commandText, List<SqlParameter> parameters = null);


        Task<object> ExecuteScallerAsync<T>(string commandText, T t) where T : class, IEntity, new();

        Task<object> ExecuteScalerAsync(string commandText, List<SqlParameter> parameters = null);


        List<T> ExecuteReader<T>(string commandText, List<SqlParameter> parameters = null) where T : class, new();

        Task<List<T>> ExecuteReaderAsync<T>(string commandText, List<SqlParameter> parameters = null) where T : class, new();

        
    }
}
