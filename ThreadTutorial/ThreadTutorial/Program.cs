using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem((e) =>
            {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            });
            Thread.CurrentThread.IsBackground = true;
            
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);                       
        }

        public static void Yaz()
        {    
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Console.ReadLine();
        }
}
   
}
