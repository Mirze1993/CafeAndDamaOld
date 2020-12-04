
using Cafe.Repostory;
using System.Linq;
using Xunit;

using Xunit.Abstractions;


namespace XUnitTest
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper output;

        public UnitTest1(ITestOutputHelper output)
        {
            MicroORM.ORMConfig.ConnectionString = "Server =.\\SQLExpress; Database = Cafe; Trusted_Connection = True";
            MicroORM.ORMConfig.DbType = MicroORM.DbType.MSSQL;
            this.output = output;
        }

       
        [Fact]
        public  void Test1()
        {
            CategoryRepository r = new CategoryRepository();
            output.WriteLine(r.RowCount().ToString());
        }

        [Fact]
        public void Test2()
        {
            CategoryRepository r = new CategoryRepository();
            var t = r.GetAll();
        }

        [Fact]
        public void Test3()
        {
            CategoryRepository r = new CategoryRepository();
            var m = r.GetByColumName("Id", 1008).FirstOrDefault();
           m.Name = "33";
            var b=r.Update(m, m.Id);
            output.WriteLine(b.ToString());
        }
        [Fact]
        public void Test4()
        {
            CategoryRepository r = new CategoryRepository();
            var m = r.GetByColumName("Id", 1008).FirstOrDefault();
            m.Name = "44";
            var b=r.Update(m, m.Id);
            output.WriteLine(b.ToString());
        }

    }
}
