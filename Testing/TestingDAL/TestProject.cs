using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using Model;
using System.Collections.Generic;

namespace Testing
{
    [TestClass]
    public class TestProject
    {
        private Project testProject;
        private ProjectDB db;

        public TestProject()
        {
            // do nothing?
        }

        #region setups + teardowns
        [ClassInitialize]
        public void setUpBeforeClass()
        {
            try
            {
                //nothing
            }
            catch
            {
                throw new Exception();
            }
        }
        [ClassCleanup]
        public void tearDownAfterClass()
        {
            try
            {
                db = null;
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
                testProject = new Project()
                {
                    // Below ID is set only for testing whether it changes when created in DB.
                    Id = 20,
                    Name = "Test",
                    Created_by_ID = "f93e4146-0ef5-45fb-8088-d1150e91dea3",
                    Contact_ID = "f93e4146-0ef5-45fb-8088-d1150e91dea3",
                    Project_status_ID = 1,
                    Project_description = "Something",
                    Street_Name = "Test street",
                    Start_time = new DateTime(2017, 04, 19, 17, 09, 21, 0),
                    Created = new DateTime(2017, 04, 19, 17, 09, 21, 0),
                    Modified = new DateTime(2017, 04, 19, 17, 09, 21, 0),
                    Deleted = false
                };
                db = new ProjectDB();
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
                testProject = null;
                db = null;
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion
        #region DALTesting
        [TestMethod]
        public void TestCreate()
        {
            int earlierID = testProject.Id;
            int returnedID = db.Create(testProject).Id;
            Assert.AreNotEqual(earlierID, returnedID);
            testProject.Id = returnedID;
        }

        [TestMethod]
        public void TestRead()
        {
            Assert.AreEqual(testProject, db.Read(testProject));
        }

        [TestMethod]
        public void TestUpdate()
        {
            testProject.Street_Name = "New street";
            Assert.IsTrue("New street".Equals(db.Update(testProject).Street_Name));
        }

        [TestMethod]
        public void TestDelete()
        {
            Assert.IsTrue(db.Delete(testProject).Deleted);
        }
        #endregion

        [TestMethod]
        public void TestReadAll()
        {
            Assert.IsTrue(0 < db.ReadAll().Count);
            List<Project> list = db.ReadAll();
            foreach(Project p in list)
            {
                Assert.IsFalse(0 == p.Id);
            }
            
        }
    }
}
