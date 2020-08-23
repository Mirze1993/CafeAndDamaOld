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
    public abstract class CRUD<T> : ExecuteCommand, ICRUD<T> where T : class, IEntity, new()
    {

        IQuery<T> query;

        public CRUD()
        {
            query = new SqlQuery<T>();
        }


        public virtual int Insert(T t)
        {
            string cmtext;
            bool b = query.Insert(out cmtext);
            if (!b) return 0;
            var id = ExecuteScaller<T>(cmtext, t);
            if (id != null) return Convert.ToInt32(id);
            else return 0;
        }

        public virtual bool Delet(int id)
        {
            string cmtext;
            bool b = query.Delete(id.ToString(), out cmtext);
            if (!b) return false;
            if (ExecuteQuery<T>(cmtext, null)) return true;
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

            var list = ExecuteReader<T>(cmtext, new List<SqlParameter>() {
                new SqlParameter("@Id",id)});
            return list;
        }

        public virtual List<T> GetAll(params string[] column)
        {
            string cmtext;
            bool b = query.GetAll(out cmtext, column);
            if (!b)
            {
                Log.AddLog("Do not create query for getAll");
            }
            return ExecuteReader<T>(cmtext);
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
            if (ExecuteQuery<T>(cmtext, t))
            {
                Log.AddLog("succes update");
                return true;
            };
            Log.AddLog("unsucces update");
            return false;
        }

        public virtual int RowCount()
        {
            string cmtext;
            bool b = query.RowCount(out cmtext);
            var o = ExecuteScaler(cmtext);

            if (o != null) return Convert.ToInt32(o);
            else return 0;
        }

        public virtual int RowCountWithSrc(string srcClm, string srcValue)
        {

            string cmtext;
            bool b = query.RowCountWithSrc(srcClm, srcValue, out cmtext);
            var o = ExecuteScaler(cmtext);
            if (o != null) return Convert.ToInt32(o);
            else return 0;
        }

        public virtual List<T> getFromTo(int from, int to)
        {
            string cmtext;
            bool b = query.getFromTo(from, to, out cmtext);
            if (!b)
            {
                Log.AddLog("Do not create query for getAll");
            }
            return ExecuteReader<T>(cmtext);
        }

        public virtual List<T> getFromToWithSrc(int from, int to, string srcClm, string srcValue)
        {
            string cmtext;
            bool b = query.getFromToWithSrc(from, to, srcClm, srcValue, out cmtext);
            if (!b)
            {
                Log.AddLog("Do not create query for getAll");
            }
            return ExecuteReader<T>(cmtext);
        }


        public virtual Task<List<T>> GetAllAsync(params string[] column)
        {
            string cmtext;
            bool b = query.GetAll(out cmtext, column);
            if (!b)
            {
                Log.AddLog("Do not create query for getAll");
            }
            return ExecuteReaderAsync<T>(cmtext);
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
            var list = ExecuteReaderAsync<T>(cmtext, new List<SqlParameter>() {
                new SqlParameter("@Id",id)});
            return list;
        }

        public virtual Task<int> InsertAsync(T t)
        {
            string cmtext;
            bool b = query.Insert(out cmtext);
            if (!b) return Task.FromResult(0);
            var id = ExecuteScallerAsync<T>(cmtext, t).Result;
            if (id != null) return Task.FromResult(Convert.ToInt32(id));
            else return Task.FromResult(0);
        }

        public virtual Task<bool> UpdateAsync(T t, int id)
        {
            string cmtext;
            bool b = query.Update(id.ToString(), out cmtext);
            if (!b)
            {
                Log.AddLog("Do not create query for insert");
                return Task.FromResult(false);
            }

            return ExecuteQueryAsync<T>(cmtext, t);
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

            return ExecuteQueryAsync<T>(cmtext,null); ;
        }

        public virtual Task<List<T>> getFromToAsync(int from, int to)
        {
            string cmtext;
            bool b = query.getFromTo(from, to, out cmtext);
            if (!b)
            {
                Log.AddLog("Do not create query for getAll");
            }
            return ExecuteReaderAsync<T>(cmtext);
        }

        public virtual Task<List<T>> getFromToWithSrcAsync(int from, int to, string srcClm, string srcValue)
        {
            string cmtext;
            bool b = query.getFromToWithSrc(from, to, srcClm, srcValue, out cmtext);
            if (!b)
            {
                Log.AddLog("Do not create query for getAll");
            }
            return ExecuteReaderAsync<T>(cmtext);
        }

    }
}
