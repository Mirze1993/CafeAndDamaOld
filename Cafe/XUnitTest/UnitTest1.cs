using BLCafe.ConcreateRepository;
using Model.Entities;
using System;
using Xunit;
using System.Data.SqlClient;
using BLCafe;
using Xunit.Abstractions;

namespace XUnitTest
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper output;

        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;
        }

        CategoryRepository category = new CategoryRepository();
        [Fact]
        public void Test1()
        {
           var list= category.GetById(30);
            list.ForEach(c => output.WriteLine(c.Name));
            output.WriteLine(Log.log);
        }

        [Fact]
        public void Test2()
        {
            var c = new Category()
            {
                Name = "21"
            };
            category.Insert(c);
            c.Name = "22";
            category.Update(c, c.Id);
            output.WriteLine(Log.log);
        }
       
        [Fact]
        public void Test3()
        {
            var c = new Category()
            {
                Name = "31"
            };
           category.Insert(c);
            c.Name = "32";
            category.Update(c, c.Id);
            output.WriteLine(Log.log);
        }

        [Fact]
        public void Test4()
        {
            var c = new Category()
            {
                Id = 99,
                Name = "41"
            };
            category.Insert(c);
            c.Name = "42";
             category.Update(c, c.Id);
            output.WriteLine(Log.log);
        }

        [Fact]
        public void Test5()
        {
            var c = new Category()
            {
                Name = "51"
            };
            category.Insert(c);
            c.Name = "52";
            category.Update(c, c.Id);
            output.WriteLine(Log.log);
        }

        [Fact]
        public void Test6()
        {
            var c = new Category()
            {
                Id = 99,
                Name = "61"
            };
            category.Insert(c);
            c.Name = "62";
            category.Update(c, c.Id);
            output.WriteLine(Log.log);
        }
    }
}
