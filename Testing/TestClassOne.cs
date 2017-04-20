using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using WCF;
using Model;

namespace Testing
{
    [TestClass]
    public class TestClassOne
    {
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
			Assert.IsTrue(true);
		}

        [TestMethod]
        public void TestSucceed()
        {
			Assert.IsTrue(true);
        }
    }
}
