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
        static  void Main(string[] args)
        {
            //Task T = new Task(Yaz);
            //T.ContinueWith((a)=> {
            //    Console.WriteLine("son" + Thread.CurrentThread.ManagedThreadId);

            //} ,TaskContinuationOptions.OnlyOnRanToCompletion);


            //Task T = Task.Factory.StartNew(Yaz);
            //T.ContinueWith((a) =>
            //{
            //    Console.WriteLine("son" + Thread.CurrentThread.ManagedThreadId);

            //}, TaskScheduler.Current);


            //Task T = Task.Run(()=> {
            //    Console.WriteLine("yaz" + Thread.CurrentThread.ManagedThreadId);
            //});



            //T.ContinueWith((a) =>
            //{
            //    Console.WriteLine("son" + Thread.CurrentThread.ManagedThreadId);

            //}, TaskScheduler.Current);

            askoron();
            Console.WriteLine("main"+Thread.CurrentThread.ManagedThreadId);           
            Console.Read();
        }
        

        public async  static Task<string> Yaz()
        {          
            Console.WriteLine("yaz" + Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(4000);
            return "aa";
        }

        public async static void  askoron()
        {
            var t = await Task.Run(Yaz);
            Console.WriteLine("gozle" + Thread.CurrentThread.ManagedThreadId);
        }

        public static void son(Task a)
        {
            Console.WriteLine("son" + Thread.CurrentThread.ManagedThreadId);
          
            
        }
    }
   
}
