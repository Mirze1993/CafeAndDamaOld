using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThreadTutorial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTutorial.Tests
{
    [TestClass()]
    public class FileManagerTests
    {
       

        [TestMethod()]
        public void WriteTextTest()
        {
            new FileManager().WriteText("write1111");
           
        }

        [TestMethod()]
        public void WriteTextTest2()
        {
            new FileManager().WriteText("write2222");
          
        }

        [TestMethod()]
        public void WriteTextTest3()
        {
            new FileManager().WriteText("write3333");
            
        }
    }
}