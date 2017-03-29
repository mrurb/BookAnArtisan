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
        static BLL.Class1 controller = null;

        [TestMethod]
        public static void setUpBeforeClass()
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
        public static void tearDownAfterClass()
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
                TestFail();
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
                TestSucceed();
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
