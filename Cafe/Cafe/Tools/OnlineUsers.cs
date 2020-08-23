using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Tools
{
    public static class OnlineUsers
    {
        public static int Count { get; set; } = 0;

        public static Dictionary<string,DateTime> Users { get; set; }

        private static readonly object obj = new object();


         

        public static void AddUser(string userName)
        {
            lock (obj)
            {
                if (Users == null) Users = new Dictionary<string, DateTime>();
                if (Users.ContainsKey(userName)) Users[userName] = DateTime.Now;
                else Users.Add(userName, DateTime.Now);
                
                var ss = Users.Where(x => (DateTime.Now - x.Value).Seconds > 10).Select(x => x.Key).ToList();
                ss.ForEach(x => Users.Remove(x));
                Count = Users.Count;
            }
        }

       
        


    }
}
