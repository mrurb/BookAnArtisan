using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BLL;
using WCF;
using Model;

namespace Testing
{
    [TestClass]
    public class TestClassOne
    {
		BLL.Class1 controller = null;

        [TestMethod]
        public void setUpBeforeClass()
        {
            
        }

        [TestMethod]
        public void tearDownAfterClass()
        {
            
        }

        [TestMethod]
        public void setUp()
        {
            
        }

        [TestMethod]
        public void tearDown()
        {
            
        }

        [TestMethod]
        public void TestFail()
        {
			Assert.IsTrue(false);
		}

        [TestMethod]
        public void TestSucceed()
        {
			Assert.IsTrue(true);
        }
    }
}
