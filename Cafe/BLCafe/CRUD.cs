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
    public abstract class CRUD<T> : SqlCommander,ICRUD<T> where T : class, IEntity, new()
    {

        IQuery<T> query;

        public CRUD()
        {
            query = new SqlQuery<T>();
        }

        public List<SqlParameter> SetParametrs(T t)
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
            return parametrs;
        }


        public virtual int Insert(T t)
        {
            string cmtext = query.Insert()+ " ;SELECT CAST(scope_identity() AS int)";
            var p=SetParametrs(t);
            var id = Scaller(cmtext, p);
            if (id != null) return Convert.ToInt32(id);
            else return 0;
        }

        public virtual bool Delet(int id)
        {
            string cmtext=query.Delete(id.ToString());            
            if (NonQuery(cmtext)) return true;
            return false;
        }

        public virtual List<T> GetById(int id)
        {
            string cmtext = query.GetById();
            
            return Reader<T>(cmtext, new List<SqlParameter>() {
                new SqlParameter("@Id",id)});            
        }

        public virtual List<T> GetAll(params string[] column)
        {
            string cmtext = query.GetAll(column);            
            return Reader<T>(cmtext);
        }

        public virtual bool Update(T t, int id)
        {

            string cmtext= query.Update(id.ToString());
            var p = SetParametrs(t);
            if (NonQuery(cmtext, p))           
                return true;           
           
            return false;
        }

        public virtual int RowCount()
        {
            string cmtext = query.RowCount();
            var o = Scaller(cmtext);
            if (o != null) return Convert.ToInt32(o);
            else return 0;
        }

        public virtual int RowCountWithSrc(string srcClm, string srcValue)
        {
            string cmtext = query.RowCountWithSrc(srcClm);
            var o = Scaller(cmtext);
            if (o != null) return Convert.ToInt32(o);
            else return 0;
        }

        public virtual List<T> getFromTo(int from, int to)
        {
            string cmtext= query.getFromTo(from, to);           
            return Reader<T>(cmtext);
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


        //public virtual Task<List<T>> GetAllAsync(params string[] column)
        //{
        //    string cmtext;
        //    bool b = query.GetAll(out cmtext, column);
        //    if (!b)
        //    {
        //        Log.AddLog("Do not create query for getAll");
        //    }
        //    return ExecuteReaderAsync<T>(cmtext);
        //}

        //public virtual Task<List<T>> GetByIdAsync(int id)
        //{
        //    string cmtext;
        //    bool b = query.GetById(out cmtext);
        //    if (!b)
        //    {
        //        Log.AddLog("Do not create query for getAll");
        //        return null;
        //    }
        //    var list = ExecuteReaderAsync<T>(cmtext, new List<SqlParameter>() {
        //        new SqlParameter("@Id",id)});
        //    return list;
        //}

        //public virtual Task<int> InsertAsync(T t)
        //{
        //    string cmtext;
        //    bool b = query.Insert(out cmtext);
        //    if (!b) return Task.FromResult(0);
        //    var id = ExecuteScallerAsync<T>(cmtext, t).Result;
        //    if (id != null) return Task.FromResult(Convert.ToInt32(id));
        //    else return Task.FromResult(0);
        //}

        //public virtual Task<bool> UpdateAsync(T t, int id)
        //{
        //    string cmtext;
        //    bool b = query.Update(id.ToString(), out cmtext);
        //    if (!b)
        //    {
        //        Log.AddLog("Do not create query for insert");
        //        return Task.FromResult(false);
        //    }

        //    return ExecuteQueryAsync<T>(cmtext, t);
        //}

        //public virtual Task<bool> DeletAsync(int id)
        //{
        //    string cmtext;
        //    bool b = query.Delete(id.ToString(), out cmtext);
        //    if (!b)
        //    {
        //        Log.AddLog("Do not create query for insert");
        //        return Task.FromResult(false);
        //    }

        //    return ExecuteQueryAsync<T>(cmtext, null); ;
        //}

        //public virtual Task<List<T>> getFromToAsync(int from, int to)
        //{
        //    string cmtext;
        //    bool b = query.getFromTo(from, to, out cmtext);
        //    if (!b)
        //    {
        //        Log.AddLog("Do not create query for getAll");
        //    }
        //    return ExecuteReaderAsync<T>(cmtext);
        //}

        //public virtual Task<List<T>> getFromToWithSrcAsync(int from, int to, string srcClm, string srcValue)
        //{
        //    string cmtext;
        //    bool b = query.getFromToWithSrc(from, to, srcClm, srcValue, out cmtext);
        //    if (!b)
        //    {
        //        Log.AddLog("Do not create query for getAll");
        //    }
        //    return ExecuteReaderAsync<T>(cmtext);
        //}

    }
}
