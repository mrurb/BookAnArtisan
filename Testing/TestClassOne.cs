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
            try
            {
                controller = new BLL.Class1();
            }
            catch
            {
                throw new Exception();
            }
        }

        [TestMethod]
        public void tearDownAfterClass()
        {
            try
            {
                controller = null;
            }
            catch
            {
                throw new Exception();
            }
        }

        [TestMethod]
        public void setUp()
        {
            try
            {
                //do nothing
            }
            catch
            {
                throw new Exception();
            }
        }

        [TestMethod]
        public void tearDown()
        {
            try
            {
                //do nothing
            }
            catch
            {
                throw new Exception();
            }
        }

        [TestMethod]
        public void TestFail()
        { 
            try
            {
                Assert.IsTrue(false);
            }
            catch
            {
                throw new Exception();
            }
        }

        [TestMethod]
        public void TestSucceed()
        {
            try
            {
                Assert.IsTrue(true);
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
