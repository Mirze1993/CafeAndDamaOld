using BLCafe.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLCafe.SqlQuery
{
    public class SqlQuery<T>:IQuery<T>
    {
        Type GetTypeT => typeof(T);

        public bool Delete(string id, out string query)
        {
            query = $"DELETE FROM {GetTypeT.Name} WHERE Id ={id} ";
            return true;
        }

        public bool GetAll(out string query, params string[] column)
        {
            query = "";
            try
            {
                if (column.Length>0)
                {
                    string clm = "";
                    foreach (var item in column)
                    {
                        clm += item + ",";
                    }
                    clm = clm.Remove(clm.Length - 1);
                    query = $"SELECT {clm}  FROM {GetTypeT.Name}";
                    return true;
                }
                else query = $"SELECT *  FROM {GetTypeT.Name}";
                return true;
            }
            catch (Exception e)
            {
                Log.AddLog(e.Message);
                return false;
            }
           
        }

        public bool GetById(out string query)
        {
            query = $"SELECT * FROM {GetTypeT.Name} WHERE Id =@Id ";
            return true;
        }

        public bool GetByIdJoin<T1>(out string query)
        {
            var nameJoin = typeof(T1).Name;
            query = $"SELECT * FROM {GetTypeT.Name} WHERE Id =@Id left join {nameJoin}.Id={GetTypeT.Name}.Id";
            return true;
        }

        public bool getFromTo(int from, int to, out string query)
        {
             query = "select * from " +
                    "  (select Row_Number() over" +
                    $"  (order by Id) as RowIndex, * from {GetTypeT.Name}) as Sub" +
                    $"  Where Sub.RowIndex >= {from} and Sub.RowIndex <= {to}";
            return true;
        }

        public bool getFromToWithSrc(int from, int to, string srcClm,string srcValue,out string query)
        {
            query = "select * from " +
                   "  (select Row_Number() over" +
                   $" (order by Id) as RowIndex, * from {GetTypeT.Name} " +
                   $" Where {srcClm} like '{srcValue}%') as Sub" +
                   $" Where Sub.RowIndex >= {from} and Sub.RowIndex <= {to}";
            return true;
        }

        public bool Insert(out string query)
        {
            string columns = "";
            string values = "";
            query = "";
            foreach (var item in GetTypeT.GetProperties())
            {
                if (item.Name == "Id") continue;
                columns += $"{item.Name} ,";
                values += $"@{item.Name} ,";
            }
            //last , remove
            if (!String.IsNullOrEmpty(columns) && !String.IsNullOrEmpty(values))
            {
                columns = columns.Remove(columns.Length - 1);
                values = values.Remove(values.Length - 1);
                if (columns.Split(",").Length != values.Split(",").Length) return false;
                query = $"INSERT INTO {GetTypeT.Name} ({columns}) VALUES({values})";
                return true;
            }
            return false;
        }

        public bool RowCount(out string query)
        {
            query = $"Select count (*) from {GetTypeT.Name}";
            return true;
        }

        public bool RowCountWithSrc(string srcClm, string srcValue, out string query)
        {
            query = $"Select count (*) from {GetTypeT.Name} u Where u.{srcClm} like '{srcValue}%'";
            return true;
        }

        public bool Update(string id,out string query)
        {
            string columns = "";
            query = "";
            foreach (var item in GetTypeT.GetProperties())
            {
                if (item.Name == "Id") continue;
                columns += $"{item.Name}=@{item.Name} ,";
            }
            if (!String.IsNullOrEmpty(columns))
            {
                columns = columns.Remove(columns.Length - 1);
                query = $"UPDATE  {GetTypeT.Name} set {columns} Where Id={id}";
                return true;
            }
            return false;
        }

        
    }
}
