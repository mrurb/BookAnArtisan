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
        private static Project testProject;
        private static ProjectDB db;

        public TestProject()
        {
            // do nothing?
        }

        #region setups + teardowns
        [ClassInitialize]
        public static void setUpBeforeClass(TestContext tc)
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
        public static void tearDownAfterClass()
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
                    CreatedBy = new User() {Id = "f93e4146-0ef5-45fb-8088-d1150e91dea3",},
                    Contact = new User() {Id= "f93e4146-0ef5-45fb-8088-d1150e91dea3"},
                    ProjectStatusID = 1,
                    ProjectDescription = "Something",
                    StreetName = "Test street",
                    StartTime = new DateTime(2017, 04, 19, 17, 09, 21, 0),
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
        public void TestCreateProject()
        {
            int earlierId = testProject.Id;
            int returnedId = db.Create(testProject).Id;
            Assert.AreNotEqual(earlierId, returnedId);
            testProject.Id = returnedId;
        }

        [TestMethod]
        public void TestReadProject()
        {
            Assert.AreEqual(testProject, db.Read(testProject));
        }

        [TestMethod]
        public void TestUpdateProject()
        {
            testProject.StreetName = "New street";
            Assert.IsTrue("New street".Equals(db.Update(testProject).StreetName));
        }

        [TestMethod]
        public void TestDeleteProject()
        {
            db.Delete(testProject);
            Assert.IsTrue(db.Read(testProject).Deleted);
        }
        #endregion

        [TestMethod]
        public void TestReadAllProject()
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
