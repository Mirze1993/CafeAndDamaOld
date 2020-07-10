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
    }
}
