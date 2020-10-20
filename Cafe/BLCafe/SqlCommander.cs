using Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BLCafe
{
    public  class SqlCommander
    {
        static readonly string connectionString = "Server=.\\SQLExpress;Database=Cafe;Trusted_Connection=True";
        SqlConnection sqlConnection;
        public SqlDataReader reader;
        SqlCommand command;
        

        
        void Begin(string CommandText, List<SqlParameter> Parameters=null)
        {
            sqlConnection = new SqlConnection(connectionString);
            command = sqlConnection.CreateCommand();
            command.CommandText = CommandText;

            if (Parameters != null) command.Parameters.AddRange(Parameters.ToArray());
            if (sqlConnection.State != ConnectionState.Open) sqlConnection.Open();  
        }

        void End()
        {
            if (!reader.IsClosed) reader.Close();
            if(command!=null)command.Dispose();

            if (sqlConnection == null) return;
            if (sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            sqlConnection.Dispose();
        }

        //nonquery
        public bool NonQuery(string CommandText, List<SqlParameter> Parameters=null)
        {
            Begin(CommandText, Parameters);
            bool b = false;
            try
            {
                b = command.ExecuteNonQuery()>0;
            }
            catch (Exception) {  }            
            End();
            return b;
        }

        //Scaller

        public object Scaller(string CommandText, List<SqlParameter> Parameters = null)
        {
            Begin(CommandText, Parameters = null);
            object b = null;
            try
            {
                b = command.ExecuteScalar();
            }
            catch (Exception) { }
            End();
            return b;
        }

        //reader
        public  T Reader<T>(Func<SqlDataReader,T> readMetod, string CommandText, List<SqlParameter> Parameters = null)
        {
            Begin(CommandText,  Parameters = null);
            try
            {
                reader = command.ExecuteReader();
            }
            catch (Exception) { End(); return default(T); }
            var r=readMetod(reader);
            End();
            return r;
        }

        public List<T> Reader<T>(string CommandText, List<SqlParameter> Parameters = null) where T : class, new()
        {
            return Reader(GetList<T>, CommandText,  Parameters = null);
        }
        
        List<T> GetList<T>(SqlDataReader r) where T : class, new()
        {            
            List<T> list = new List<T>();
            if (r == null) return list;

            while (r.Read())
            {
                T t = new T();
                foreach (var item in typeof(T).GetProperties())
                {
                    try
                    {
                        var value = r[item.Name];
                        item.SetValue(t, value);
                    }
                    catch { }
                }
                list.Add(t);
            }
            return list;
        }

    }
}
