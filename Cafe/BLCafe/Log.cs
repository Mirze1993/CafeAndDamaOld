using System;
using System.Collections.Generic;
using System.Text;

namespace BLCafe
{
    public static class  Log
    {
        public static string log="";
        public static void AddLog(string msg)
        {
            log +=DateTime.Now.ToString()+ msg + "\n";
        }
    }
}
