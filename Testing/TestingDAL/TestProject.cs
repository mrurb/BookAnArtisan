using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using Model;

namespace Testing
{
    [TestClass]
    public class TestProject
    {
        private Project testProject;
        private ProjectDB db;

        public TestProject()
        {
            testProject = new Project()
            {
                // Below ID is set only for testing whether it changes when created in DB.
                ID = 20,
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

        

        #region DALTesting
        [TestMethod]
        public void TestCreate()
        {
            int earlierID = testProject.ID;
            int returnedID = db.Create(testProject).ID;
            Assert.AreNotEqual(earlierID, returnedID);
            testProject.ID = returnedID;
        }

        [TestMethod]
        public void TestRead()
        {
            TestCreate();
            Assert.IsTrue(testProject.Equals(db.Read(testProject)));
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
    }
}
