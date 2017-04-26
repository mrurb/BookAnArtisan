using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.TestingDAL
{
    [TestClass]
    public class TestStatus
    {
        static private Status testStatus;
        static private StatusDB db;

        public TestStatus()
        {
            // do nothing?
        }
        #region setups + teardowns
        [ClassInitialize]
        public static void setUpBeforeClass(TestContext tc)
        {
            try
            {
                // nothing?
            }
            catch
            {
                throw new Exception();
            }
        }

        [ClassCleanup]
        public static void tearDownAfterClass()
        {
            try
            {
                db = null;
                testStatus = null;
            }
            catch
            {
                throw new Exception();
            }
        }

        [TestInitialize]
        public void setUp()
        {
            try
            {
                db = new StatusDB();
                testStatus = new Status { Id = 1, Name = "OK" };
            }
            catch
            {
                throw new Exception();
            }
        }

        [TestCleanup]
        public void tearDown()
        {
            try
            {
                db = null;
                testStatus = null;
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion
        [TestMethod]
        public void TestCreate()
        {
            Assert.IsNotNull(db.Create(testStatus));
        }

        [TestMethod]
        public void TestRead()
        {
            Assert.AreEqual(testStatus, db.Read(testStatus));
        }

        [TestMethod]
        public void TestUpdate()
        {
            testStatus.Name = "New name";
            Assert.AreEqual("New name", db.Update(testStatus).Name);
        }

        [TestMethod]
        public void TestDelete()
        {
            Assert.AreEqual(testStatus, db.Delete(testStatus));
        }

        [TestMethod]
        public void TestReadAll()
        {
            Assert.IsTrue(0 < db.ReadAll().Count);
            List<Status> list = db.ReadAll();
            foreach (Status s in list)
            {
                Assert.IsTrue(s.Id > 0);
            }
        }
    }
}
