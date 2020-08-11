using BLCafe.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BLCafe
{
    public sealed class CreateSqlConnection : IDisposable, IConnection
    {
        static readonly string connectionstring = "Server=.\\SQLExpress;Database=Cafe;Trusted_Connection=True";

        private static readonly object lockobj = new object();

        private static SqlConnection connection = null;

        /// <summary>
        /// create connection
        /// </summary>
        public SqlConnection Connection
        {
            get
            {
                lock (lockobj)
                {
                    if (connection == null)
                    {
                        connection = new SqlConnection(connectionstring);
                    }
                    return connection;
                }
            }
        }

        /// <summary>
        /// Connection Open
        /// </summary>
        public void Open()
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
        }

        /// <summary>
        /// Connection OpenAsc
        /// </summary>
        public async Task OpenAsync()
        {
            if (Connection.State != ConnectionState.Open)
            {
                await Connection.OpenAsync();
            }
        }


        /// <summary>
        /// bregin Transaction
        /// </summary>
        public SqlTransaction BeginTransaction(string transactionName)
        {
            return Connection.BeginTransaction(IsolationLevel.Serializable, transactionName);
        }

        /// <summary>
        /// bregin TransactionAsync
        /// </summary>
        public async Task<SqlTransaction> BeginTransactionAsync(string transactionName)
        {
            SqlTransaction transaction = await Task.Run<SqlTransaction>(
                () => { return Connection.BeginTransaction(IsolationLevel.Serializable, transactionName); }
                );
            return transaction;
        }
        /// <summary>
        /// ExecuteQuery Excecuete
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool ExecuteQuery(string commandText, List<SqlParameter> parameters, SqlTransaction sqlTransaction = null)
        {
            SqlCommand command = Connection.CreateCommand();
            try
            {
                
                if (sqlTransaction != null) command.Transaction = sqlTransaction;
                command.CommandText = commandText;

                if (parameters != null) command.Parameters.AddRange(parameters.ToArray());

                Open();
                return command.ExecuteNonQuery() > 0 ? true : false;
            }
            catch (Exception e)
            {
                Log.AddLog(e.Message); return false;
            }
            finally
            {
                command.Dispose();
            }
        }

        /// <summary>
        /// ExecuteQuery ExcecueteAsync 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteQueryAsync(string commandText, List<SqlParameter> parameters, SqlTransaction sqlTransaction = null)
        {
            SqlCommand command = Connection.CreateCommand();
            Log.AddLog("command create");
            try
            {
                if (sqlTransaction != null) command.Transaction = sqlTransaction;
                command.CommandText = commandText;

                if (parameters != null) command.Parameters.AddRange(parameters.ToArray());
                await OpenAsync();
                return await command.ExecuteNonQueryAsync() > 0 ? true : false;
            }
            catch (Exception e)
            {
                Log.AddLog(e.Message); return false;
            }
            finally
            {
                command.Dispose();
            }
        }


        public List<T> ExecuteReader<T>(string commandText, List<SqlParameter> parameters = null) where T : class, new()
        {
            List<T> list = new List<T>();
            var command = Connection.CreateCommand();
            command.CommandText = commandText;
            if (parameters != null) command.Parameters.AddRange(parameters.ToArray());
            Open();
            try
            {
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

            }
            catch (Exception e)
            {
                return new List<T>();
            }
            finally
            {
                command.Dispose();
            }
            return list;
        }

        public async Task<List<T>> ExecuteReaderAsync<T>(string commandText, List<SqlParameter> parameters = null) where T : class, new()
        {
            List<T> list = new List<T>();
            var command = Connection.CreateCommand();
            command.CommandText = commandText;
            if (parameters != null) command.Parameters.AddRange(parameters.ToArray());
            await OpenAsync();
            try
            {
                var result = await command.ExecuteReaderAsync();
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

            }
            catch (Exception ee)
            {
                string a = ee.Message;
                return new List<T>();
            }
            finally
            {
                command.Dispose();
            }
            return list;
        }

        public object ExecuteScaler(string commandText, List<SqlParameter> parameters = null)
        {
            object obj = null;
            var command = Connection.CreateCommand();
            command.CommandText = commandText;
            if (parameters != null) command.Parameters.AddRange(parameters.ToArray());
            Open();
            try
            {
                obj = command.ExecuteScalar();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                command.Dispose();
            }
            return obj;
        }

        public async Task<object> ExecuteScalerAsync(string commandText, List<SqlParameter> parameters = null)
        {
            object obj = null;
            var command = Connection.CreateCommand();
            command.CommandText = commandText;
            if (parameters != null) command.Parameters.AddRange(parameters.ToArray());
             await OpenAsync();
            try
            {
                obj =await command.ExecuteScalarAsync();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                command.Dispose();
            }
            return obj;
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
                Log.AddLog("connection close");
            }
        }
    }

}
