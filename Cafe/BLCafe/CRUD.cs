using BLCafe.Interface;
using BLCafe.SqlQuery;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BLCafe
{
    public abstract class CRUD<T> : ICRUD<T> where T : class,IEntity, new()
    {
        Type GetTypeT => typeof(T);
        IQuery<T> query;

        public CRUD()
        {
            query = new SqlQuery<T>();
        }

        bool ExecuteQuery( string commandText, T t=null)
        {
            using (CreateSqlConnection connection = new CreateSqlConnection())
            {
                List<SqlParameter> parametrs = new List<SqlParameter>();
                if(t!=null)
                foreach (var item in GetTypeT.GetProperties())
                {                    
                    if (item.Name == "Id") continue;
                    object value = item.GetValue(t);
                    if (value == null) value = DBNull.Value;
                    parametrs.Add(new SqlParameter($"@{item.Name}", value));
                }

                return connection.ExecuteQuery(commandText,parametrs);
               
            }
        }

        object ExecuteScaller(string commandText, T t = null)
        {
            using (CreateSqlConnection connection = new CreateSqlConnection())
            {
                List<SqlParameter> parametrs = new List<SqlParameter>();
                if (t != null)
                    foreach (var item in GetTypeT.GetProperties())
                    {
                        if (item.Name == "Id") continue;
                        object value = item.GetValue(t);
                        if (value == null) value = DBNull.Value;
                        parametrs.Add(new SqlParameter($"@{item.Name}", value));
                    }

                return connection.ExecuteScaler(commandText, parametrs);

            }
        }

        public List<T> ExecuteReader(string commandText, int id = -1)
        {           
            using (CreateSqlConnection connection = new CreateSqlConnection())
            {
                List<SqlParameter> parametrs = null;
                if (id > 0) parametrs=new List<SqlParameter>() {new SqlParameter("@Id",id) };
                
                return connection.ExecuteReader<T>(commandText, parametrs);
               
            }
        }


        async Task<bool> ExecuteQueryAsync(string commandText, T t = null)
        {
            using (CreateSqlConnection connection = new CreateSqlConnection())
            {
                List<SqlParameter> parametrs = new List<SqlParameter>();
                if(t!=null)
                foreach (var item in GetTypeT.GetProperties())
                {
                    if (item.Name == "Id") continue;
                    object value = item.GetValue(t);
                    if (value == null) value = DBNull.Value;
                    parametrs.Add(new SqlParameter($"@{item.Name}", value));
                }
                
                return await connection.ExecuteQueryAsync(commandText, parametrs);
                
            }
        }

        async Task<object> ExecuteScallerAsync(string commandText, T t = null)
        {
            using (CreateSqlConnection connection = new CreateSqlConnection())
            {
                List<SqlParameter> parametrs = new List<SqlParameter>();
                if (t != null)
                    foreach (var item in GetTypeT.GetProperties())
                    {
                        if (item.Name == "Id") continue;
                        object value = item.GetValue(t);
                        if (value == null) value = DBNull.Value;
                        parametrs.Add(new SqlParameter($"@{item.Name}", value));
                    }

                return await connection.ExecuteScalerAsync(commandText, parametrs);

            }
        }

        public async  Task<List<T>> ExecuteReaderAsync(string commandText, int id = -1)
        {
            
            using (CreateSqlConnection connection = new CreateSqlConnection())
            {
                List<SqlParameter> parametrs = null;
                if (id > 0) parametrs = new List<SqlParameter>() { new SqlParameter("@Id", id) };

                return await connection.ExecuteReaderAsync<T>(commandText, parametrs);
            }
        }
        

        public virtual  int  Insert(T t)
        {
            string cmtext;
            bool b = query.Insert(out cmtext);
            if (!b) return 0;
            var id= ExecuteScaller(cmtext, t);
            if (id != null) return Convert.ToInt32(id);
            else return 0;
        }

        public virtual bool Delet(int id)
        {
            string cmtext;
            bool b = query.Delete(id.ToString(), out cmtext);
            if (!b)return false;
            if (ExecuteQuery(cmtext))return true;
            return false;
        }

        public virtual List<T> GetById(int id)
        {
            string cmtext;
            bool b = query.GetById(out cmtext);
            if (!b)
            {
                Log.AddLog("Do not create query for getAll");
            }
            var list = ExecuteReader(cmtext, id);
            return list;
        }

        public virtual List<T> GetAll(params string[] column)
        {
            string cmtext;
            bool b = query.GetAll(out cmtext,column);
            if (!b)
            {
                Log.AddLog("Do not create query for getAll");
            }
            return ExecuteReader(cmtext);
        }

        public virtual bool Update(T t, int id)
        {

            string cmtext;
            bool b = query.Update(id.ToString(), out cmtext);
            if (!b)
            {
                Log.AddLog("Do not create query for update");
                return false;
            }
            if (ExecuteQuery(cmtext,t))
            {
                Log.AddLog("succes update");
                return true;
            };
            Log.AddLog("unsucces update");
            return false;
        }

        public virtual int RowCount()
        {
            object o;
            using (CreateSqlConnection connection = new CreateSqlConnection())
            {
                string cmtext;
                bool b = query.RowCount(out cmtext);
                o = connection.ExecuteScaler(cmtext);
            }
            if (o != null) return Convert.ToInt32(o);
            else return 0;
        }

        public virtual int RowCountWithSrc(string srcClm, string srcValue)
        {
            object o;
            using (CreateSqlConnection connection = new CreateSqlConnection())
            {
                string cmtext;
                bool b = query.RowCountWithSrc(srcClm,srcValue,out cmtext);
                o = connection.ExecuteScaler(cmtext);
            }
            if (o != null) return Convert.ToInt32(o);
            else return 0;
        }

        public virtual List<T> getFromTo(int from, int to)
        {
            string cmtext;
            bool b = query.getFromTo(from,to,out cmtext);
            if (!b)
            {
                Log.AddLog("Do not create query for getAll");
            }
            return ExecuteReader(cmtext);
        }

        public virtual List<T> getFromToWithSrc(int from, int to, string srcClm, string srcValue)
        {
            string cmtext;
            bool b = query.getFromToWithSrc(from, to, srcClm, srcValue, out cmtext);
            if (!b)
            {
                Log.AddLog("Do not create query for getAll");
            }
            return ExecuteReader(cmtext);
        }

        


        public virtual Task<List<T>> GetAllAsync(params string[] column)
        {
            string cmtext;
            bool b = query.GetAll(out cmtext, column);
            if (!b)
            {
                Log.AddLog("Do not create query for getAll");
            }
            return ExecuteReaderAsync(cmtext);
        }

        public virtual Task<List<T>> GetByIdAsync(int id)
        {
            string cmtext;
            bool b = query.GetById(out cmtext);
            if (!b)
            {
                Log.AddLog("Do not create query for getAll");
                return null;
            }
            var list = ExecuteReaderAsync(cmtext, id);
            return list;
        }

        public virtual Task<int> InsertAsync(T t)
        {
            string cmtext;
            bool b = query.Insert(out cmtext);
            if (!b) return Task.FromResult(0);
            var id = ExecuteScaller(cmtext, t);
            if (id != null) return Task.FromResult(Convert.ToInt32(id));
            else return Task.FromResult(0);

        }

        public virtual Task<bool> UpdateAsync(T t, int id)
        {
            string cmtext;
            bool b = query.Update(id.ToString(),out cmtext);
            if (!b)
            {
                Log.AddLog("Do not create query for insert");
                return Task.FromResult(false);
            }

            return ExecuteQueryAsync(cmtext,t);
        }

        public virtual Task<bool> DeletAsync(int id)
        {
            string cmtext;
            bool b = query.Delete(id.ToString(), out cmtext);
            if (!b)
            {
                Log.AddLog("Do not create query for insert");
                return Task.FromResult(false);
            }

            return ExecuteQueryAsync(cmtext); ;
        }
        
        public virtual Task<List<T>> getFromToAsync(int from, int to)
        {
            string cmtext;
            bool b = query.getFromTo(from, to, out cmtext);
            if (!b)
            {
                Log.AddLog("Do not create query for getAll");
            }
            return ExecuteReaderAsync(cmtext);
        }
         
        public virtual Task<List<T>> getFromToWithSrcAsync(int from, int to, string srcClm, string srcValue)
        {
            string cmtext;
            bool b = query.getFromToWithSrc(from, to, srcClm, srcValue, out cmtext);
            if (!b)
            {
                Log.AddLog("Do not create query for getAll");
            }
            return ExecuteReaderAsync(cmtext);
        }

    }
}
