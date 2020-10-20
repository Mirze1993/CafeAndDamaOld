using BLCafe.ConcreateRepository;
using Model.Entities;
using System;
using Xunit;
using System.Data.SqlClient;
using BLCafe;
using Xunit.Abstractions;
using System.Threading.Tasks;

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
        ProductRepository product = new ProductRepository();
        [Fact]
        public  void Test1()
        {
            var g = new Class1("select * from Games").ExecuteReadCommand<Games>();
            

            foreach (var item in g)
            {
                output.WriteLine(item.Name.ToString());
            }
        }

        [Fact]
        public  void Test2()
        {
            var list = category.GetAll();
            foreach (var item in list)
            {
                output.WriteLine(item.Name.ToString());
            }
           
        }

        [Fact]
        public void Test3()
        {
            var list = product.GetById(30);

            foreach (var item in list)
            {
                output.WriteLine(item.Name.ToString());
            }
        }

        [Fact]
        public void Test4()
        {
            var list = category.GetById(30);

            foreach (var item in list)
            {
                output.WriteLine(item.Name.ToString());
            }
        }


        [Fact]
        public async Task Test5()
        {
            var list = await category.GetByIdAsync(30);

            foreach (var item in list)
            {
                output.WriteLine(item.Name.ToString());
            }
        }

        [Fact]
        public async Task Test6()
        {
            var list = await category.GetByIdAsync(30);

            foreach (var item in list)
            {
                output.WriteLine(item.Name.ToString());
            }
        }

        [Fact]
        public async Task  Test7()
        {
            var list = await category.GetByIdAsync(30);

            foreach (var item in list)
            {
                output.WriteLine(item.Name.ToString());
            }
        }

    }
}
