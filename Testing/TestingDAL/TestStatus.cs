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
        static private StatusDb db;

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
                db = new StatusDb();
                testStatus = new Status { Id = 2, Name = "OK" };
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
        public void TestCreateStatus()
        {
            Assert.IsNotNull(db.Create(testStatus));
        }

        [TestMethod]
        public void TestReadStatus()
        {
            Assert.AreEqual(testStatus, db.Read(testStatus));
        }

        [TestMethod]
        public void TestUpdateStatus()
        {
            testStatus.Name = "New name";
            db.Update(testStatus);
            Assert.AreEqual(testStatus.Name, db.Read(testStatus).Name);
        }

        [TestMethod]
        public void TestDeleteStatus()
        {
            Assert.AreEqual(db.Delete(testStatus), null); // fix later TODO
        }

        [TestMethod]
        public void TestReadAllStatus()
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
