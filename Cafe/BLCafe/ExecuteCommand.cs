using BLCafe.Interface;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BLCafe
{
    public class ExecuteCommand : IExecuteCommand
    {
        static readonly string connectionString = "Server=.\\SQLExpress;Database=Cafe;Trusted_Connection=True";
        
        public bool ExecuteQuery<T>(string commandText, T t) where T : class, IEntity, new()
        {
            List<SqlParameter> parametrs = new List<SqlParameter>();
            if (t != null)
                foreach (var item in typeof(T).GetProperties())
                {
                    if (item.Name == "Id") continue;
                    object value = item.GetValue(t);
                    if (value == null) value = DBNull.Value;
                    parametrs.Add(new SqlParameter($"@{item.Name}", value));
                }

            return ExecuteQueryy(commandText, parametrs);

        }
        public bool ExecuteQueryy(string commandText, List<SqlParameter> parameters)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand command = sqlConnection.CreateCommand();
                try
                {
                    command.CommandText = commandText;
                    if (parameters != null) command.Parameters.AddRange(parameters.ToArray());
                    if (sqlConnection.State != ConnectionState.Open) sqlConnection.Open();
                    var b = command.ExecuteNonQuery() > 0;
                    if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
                    return b;
                }
                catch (Exception e)
                {
                    if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
                    return false;
                }
                finally
                {
                    command.Dispose();
                }
            }

        }

        public async Task<bool> ExecuteQueryAsync<T>(string commandText, T t) where T : class, IEntity, new()
        {

            List<SqlParameter> parametrs = new List<SqlParameter>();
            if (t != null)
                foreach (var item in typeof(T).GetProperties())
                {
                    if (item.Name == "Id") continue;
                    object value = item.GetValue(t);
                    if (value == null) value = DBNull.Value;
                    parametrs.Add(new SqlParameter($"@{item.Name}", value));
                }

            return await ExecuteQueryAsyncc(commandText, parametrs);

        }
        public async Task<bool> ExecuteQueryAsyncc(string commandText, List<SqlParameter> parameters)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand command = sqlConnection.CreateCommand();
                Log.AddLog("command create");
                try
                {
                    command.CommandText = commandText;
                    if (parameters != null) command.Parameters.AddRange(parameters.ToArray());
                    if (sqlConnection.State != ConnectionState.Open) await sqlConnection.OpenAsync();
                    var b = await command.ExecuteNonQueryAsync() > 0;
                    if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
                    return b;
                }
                catch (Exception e)
                {
                    if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
                    return false;
                }
                finally
                {
                    command.Dispose();
                }
            }

        }


        public object ExecuteScaller<T>(string commandText, T t) where T : class, IEntity, new()
        {
            List<SqlParameter> parametrs = new List<SqlParameter>();
            if (t != null)
                foreach (var item in typeof(T).GetProperties())
                {
                    if (item.Name == "Id") continue;
                    object value = item.GetValue(t);
                    if (value == null) value = DBNull.Value;
                    parametrs.Add(new SqlParameter($"@{item.Name}", value));
                }
            return ExecuteScaler(commandText, parametrs);

        }
        public object ExecuteScaler(string commandText, List<SqlParameter> parameters = null)
        {
            object obj = null;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var command = sqlConnection.CreateCommand();
                command.CommandText = commandText;

                try
                {
                    if (parameters != null) command.Parameters.AddRange(parameters.ToArray());
                    if (sqlConnection.State != ConnectionState.Open) sqlConnection.Open();
                    obj = command.ExecuteScalar();
                    if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
                }
                catch (Exception e)
                {
                    if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
                    return null;
                }
                finally
                {
                    command.Dispose();
                }
                return obj;
            }

        }


        public async Task<object> ExecuteScallerAsync<T>(string commandText, T t) where T : class, IEntity, new()
        {
            List<SqlParameter> parametrs = new List<SqlParameter>();
            if (t != null)
                foreach (var item in typeof(T).GetProperties())
                {
                    if (item.Name == "Id") continue;
                    object value = item.GetValue(t);
                    if (value == null) value = DBNull.Value;
                    parametrs.Add(new SqlParameter($"@{item.Name}", value));
                }

            return await ExecuteScalerAsync(commandText, parametrs);

        }
        public async Task<object> ExecuteScalerAsync(string commandText, List<SqlParameter> parameters = null)
        {
            object obj = null;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var command = sqlConnection.CreateCommand();
                command.CommandText = commandText;

                try
                {
                    if (parameters != null) command.Parameters.AddRange(parameters.ToArray());
                    if (sqlConnection.State != ConnectionState.Open) await sqlConnection.OpenAsync();
                    obj = await command.ExecuteScalarAsync();
                    if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
                }
                catch (Exception e)
                {
                    if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
                    return null;
                }
                finally
                {
                    command.Dispose();
                }
                return obj;
            }
        }

        public List<T> ExecuteReader<T>(string commandText, List<SqlParameter> parameters = null) where T : class, new()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString)) 
            {
                List<T> list = new List<T>();
                var command = sqlConnection.CreateCommand();
                command.CommandText = commandText;              
                try
                {
                    if (parameters != null) command.Parameters.AddRange(parameters.ToArray());
                    if (sqlConnection.State != ConnectionState.Open) sqlConnection.Open();
                    var result = command.ExecuteReader();
                    while (result.Read())
                    {
                        T t = new T();
                        foreach (var item in typeof(T).GetProperties())
                        {
                            try
                            {
                                var value = result[item.Name];
                                item.SetValue(t, value);
                            }
                            catch { }
                        }
                        list.Add(t);
                    }
                    result.Close();
                    if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();

                }
                catch (Exception e)
                {
                    if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
                    return new List<T>();
                }
                finally
                {
                    command.Dispose();
                }
                return list;
            }

        }
        public async Task<List<T>> ExecuteReaderAsync<T>(string commandText, List<SqlParameter> parameters = null) where T : class, new()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                List<T> list = new List<T>();
                var command = sqlConnection.CreateCommand();
                command.CommandText = commandText;
                try
                {
                    if (parameters != null) command.Parameters.AddRange(parameters.ToArray());
                    if (sqlConnection.State != ConnectionState.Open) await sqlConnection.OpenAsync();
                    var result = await command.ExecuteReaderAsync();
                    while (await result.ReadAsync())
                    {
                        T t = new T();
                        foreach (var item in typeof(T).GetProperties())
                        {
                            try
                            {
                                var value = result[item.Name];
                                item.SetValue(t, value);
                            }
                            catch { }
                        }
                        list.Add(t);
                    }
                    result.Close();
                    if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();

                }
                catch (Exception e)
                {
                    if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
                    return new List<T>();
                }
                finally
                {
                    command.Dispose();
                }
                return list;
            }
        }





        //public SqlTransaction BeginTransaction(string transactionName)
        //{
        //    return sqlConnection.BeginTransaction(IsolationLevel.Serializable, transactionName);
        //}
        //public async Task<SqlTransaction> BeginTransactionAsync(string transactionName)
        //{
        //    SqlTransaction transaction = await Task.Run<SqlTransaction>(
        //        () => { return sqlConnection.BeginTransaction(IsolationLevel.Serializable, transactionName); }
        //        );
        //    return transaction;
        //}


    }
}
