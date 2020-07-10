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
                        Log.AddLog("create connection");
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
                Log.AddLog("connection open");
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
                Log.AddLog("connection open");
            }
        }


        /// <summary>
        /// bregin Transaction
        /// </summary>
        public SqlTransaction BeginTransaction(string transactionName)
        {
            return connection.BeginTransaction(IsolationLevel.Serializable, transactionName);
        }

        /// <summary>
        /// bregin TransactionAsync
        /// </summary>
        public async Task<SqlTransaction> BeginTransactionAsync(string transactionName)
        {
            SqlTransaction transaction = await Task.Run<SqlTransaction>(
                () => { return connection.BeginTransaction(IsolationLevel.Serializable, transactionName); }
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
            SqlCommand command = connection.CreateCommand();
            Log.AddLog("command create");
            try
            {
                if (sqlTransaction != null) command.Transaction = sqlTransaction;
                command.CommandText = commandText;

                if (parameters != null) command.Parameters.AddRange(parameters.ToArray());

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
        public async Task<bool> ExecuteQueryAsync(string commandText,  List<SqlParameter> parameters, SqlTransaction sqlTransaction = null)
        {
            SqlCommand command = connection.CreateCommand();
            Log.AddLog("command create");
            try
            {
                if (sqlTransaction != null) command.Transaction = sqlTransaction;
                command.CommandText = commandText;

                if (parameters != null) command.Parameters.AddRange(parameters.ToArray());
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
