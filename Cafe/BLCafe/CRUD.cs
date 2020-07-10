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

        bool ExecuteQuery(T t, string commandText)
        {
            using (CreateSqlConnection connection = new CreateSqlConnection())
            {
                List<SqlParameter> parametrs = new List<SqlParameter>();
                foreach (var item in GetTypeT.GetProperties())
                {                    
                    if (item.Name == "Id") continue;
                    object value = item.GetValue(t);
                    if (value == null) value = DBNull.Value;
                    parametrs.Add(new SqlParameter($"@{item.Name}", value));
                }
                connection.Open();
                try
                {
                    return connection.ExecuteQuery(commandText,parametrs);
                }
                catch (Exception e)
                {
                    Log.AddLog(e.Message); return false;
                }
            }
        }

        protected List<T> ExecuteReader(string commandText, int id = -1)
        {
            List<T> list = new List<T>();
            using (CreateSqlConnection connection = new CreateSqlConnection())
            {
                SqlCommand command = new SqlCommand(commandText, connection.Connection);
                Log.AddLog("command create");
                if (id > 0) command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                try
                {
                    var reader=command.ExecuteReader();
                    while (reader.Read())
                    {
                        T t = new T();
                        foreach (var item in GetTypeT.GetProperties())
                        {
                           var value= reader[item.Name];
                           item.SetValue(t, value);
                        }
                        list.Add(t);
                    }
                    reader.Close();
                    Log.AddLog("succes get");
                    return list;
                }
                catch (Exception e)
                {
                    Log.AddLog(e.Message); return null;
                }
                finally
                {
                    command.Dispose();
                }
            }
        }

        async Task<bool> ExecuteQueryAsync(T t, string commandText)
        {
            using (CreateSqlConnection connection = new CreateSqlConnection())
            {
                List<SqlParameter> parametrs = new List<SqlParameter>();
                foreach (var item in GetTypeT.GetProperties())
                {
                    if (item.Name == "Id") continue;
                    object value = item.GetValue(t);
                    if (value == null) value = DBNull.Value;
                    parametrs.Add(new SqlParameter($"@{item.Name}", value));
                }

                await connection.OpenAsync();
                try
                {
                    return await connection.ExecuteQueryAsync(commandText, parametrs);
                }
                catch (Exception e)
                {
                    Log.AddLog(e.Message); return false;
                }
            }
        }

        protected async  Task<List<T>> ExecuteReaderAsync(string commandText, int id = -1)
        {
            List<T> list = new List<T>();
            using (CreateSqlConnection connection = new CreateSqlConnection())
            {
                SqlCommand command = new SqlCommand(commandText, connection.Connection);
                Log.AddLog("command create");

                if (id > 0) command.Parameters.AddWithValue("@Id", id);
                
                await connection.OpenAsync();
                try
                {
                    var reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        T t = new T();
                        foreach (var item in GetTypeT.GetProperties())
                        {
                            var value = reader[item.Name];
                            item.SetValue(t, value);
                        }
                        list.Add(t);
                    }
                    reader.Close();
                    Log.AddLog("succes get");
                    return list;
                }
                catch (Exception e)
                {
                    Log.AddLog(e.Message); return null;
                }
                finally
                {
                    command.Dispose();
                }
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
            

            if (ExecuteQuery(t, cmtext))
            {
                Log.AddLog("succes insert");
                return true;
            };
            Log.AddLog("unsucces insert");
            return false;
        }

        public bool Delet(int id)
        {
            throw new NotImplementedException();
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
            if (ExecuteQuery(t, cmtext))
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

            return ExecuteQueryAsync(t, cmtext);
            
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

            return ExecuteQueryAsync(t, cmtext);
        }

        public Task<bool> DeletAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
