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
    public class CRUD<T> : ICRUD<T> where T : class,IEntity, new()
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

        protected List<T> ExecuteReader(string commandText, int id = -1)
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

        protected async  Task<List<T>> ExecuteReaderAsync(string commandText, int id = -1)
        {
            
            using (CreateSqlConnection connection = new CreateSqlConnection())
            {
                List<SqlParameter> parametrs = null;
                if (id > 0) parametrs = new List<SqlParameter>() { new SqlParameter("@Id", id) };

                return await connection.ExecuteReaderAsync<T>(commandText, parametrs);
            }
        }
        

        public bool  Insert(T t)
        {
           
            string cmtext;
            bool b = query.Insert(out cmtext);
            if (!b)
            {
                Log.AddLog("Do not create query for insert");
                return false;
            }
            

            if (ExecuteQuery(cmtext,t))
            {
                Log.AddLog("succes insert");
                return true;
            };
            Log.AddLog("unsucces insert");
            return false;
        }

        public bool Delet(int id)
        {
            string cmtext;
            bool b = query.Delete(id.ToString(), out cmtext);
            if (!b)return false;
            if (ExecuteQuery(cmtext))return true;
            return false;
        }

        public List<T> GetById(int id)
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

        public List<T> GetAll(params string[] column)
        {
            string cmtext;
            bool b = query.GetAll(out cmtext,column);
            if (!b)
            {
                Log.AddLog("Do not create query for getAll");
            }
            return ExecuteReader(cmtext);
        }

        public bool Update(T t, int id)
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




        public Task<List<T>> GetAllAsync(params string[] column)
        {
            string cmtext;
            bool b = query.GetAll(out cmtext, column);
            if (!b)
            {
                Log.AddLog("Do not create query for getAll");
            }
            return ExecuteReaderAsync(cmtext);
        }

        public Task<List<T>> GetByIdAsync(int id)
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

        public Task<bool> InsertAsync(T t)
        {
            string cmtext;
            bool b = query.Insert(out cmtext);
            if (!b)
            {
                Log.AddLog("Do not create query for insert");
                return Task.FromResult<bool>(false);
            }

            return ExecuteQueryAsync(cmtext,t);
            
        }

        public Task<bool> UpdateAsync(T t, int id)
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

        public Task<bool> DeletAsync(int id)
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
    }
}
