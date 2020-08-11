using System;
using System.Collections.Generic;
using System.Text;

namespace BLCafe.Interface
{
    interface IQuery<T>
    {
       bool Insert(out string query);
       bool Update(string id,out string query);
       bool GetAll(out string query, params string[] column);
       bool GetById(out string query);
       bool Delete(string id, out string query);
       bool RowCount(out string query);
       bool RowCountWithSrc(string srcClm, string srcValue, out string query);
       bool getFromTo(int from, int to,out string query);
       bool getFromToWithSrc(int from, int to, string srcClm, string srcValue, out string query);
      
    }
}
