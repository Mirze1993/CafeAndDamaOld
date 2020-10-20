using BLCafe;
using BLCafe.ConcreateRepository;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //CategoryRepository category = new CategoryRepository();
            //var c= new Category()
            //{
            //    Name = "mmmm"
            //};
            // category.Insert(c);
            //c.Name = "yyyy";
            //category.Update(c,c.Id);

            //Console.WriteLine(Log.log);
            // var list = category.GetAll();
            //SubCategoryRepository subCategory = new SubCategoryRepository();
            //var list = subCategory.GetUISubCategories();

            //list.ForEach(c =>Console.WriteLine(c.Name+" -> "+c.CategoryName));

            //Console.ReadLine();

            //HashTest hashTest = new HashTest();
            //string a;
            //Console.WriteLine(a= hashTest.CreateHashString("mirze"));

            //Console.WriteLine(hashTest.Verify("mirze", a));
            //Console.WriteLine(hashTest.Verify("mirrze", a));
            //Console.WriteLine(hashTest.Verify("mirzere", a));
            //Console.WriteLine(hashTest.Verify("mirzgere", a));
            //Console.WriteLine(hashTest.Verify("mirze", a));

            //select(x => (x.Name == "mirze").ToString());
           
           
            Console.ReadLine();
        }


        public static void select(Expression <Func<Category,string>> t)
        {
            //PropertyInfo p;
            //if (t.Body is MemberExpression)
            //{
            //    p = (t.Body as MemberExpression).Member as PropertyInfo;
            //}
            //else if(t.Body is UnaryExpression)
            //{
            //    var tt = ((UnaryExpression)t.Body).Operand as MemberExpression;
            //}


            //var m=t.Body as BinaryExpression;
           
            

            Console.WriteLine(t.Body);
            //Console.WriteLine(m.Right);
            
        }

    }
}
