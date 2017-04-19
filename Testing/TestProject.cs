using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using Model;

namespace Testing
{
    [TestClass]
    public class TestProject
    {
        #region DALTesting
        [TestMethod]
        public void TestCreate()
        {
            ProjectDB db = new ProjectDB();

            Assert.AreNotEqual(0, db.Create(new Project()
            {
                Name = "T",
                Created_by_ID = "f93e4146-0ef5-45fb-8088-d1150e91dea3",
                Contact_ID = "f93e4146-0ef5-45fb-8088-d1150e91dea3",
                Project_status_ID = 1,
                Project_description = "Something",
                Street_Name = "V",
                Start_time = DateTime.Now,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                Deleted = false
            }).ID);
        }

        [TestMethod]
        public void TestRead()
        {
        }

        [TestMethod]
        public void TestUpdate()
        {
        }

        [TestMethod]
        public void TestDelete()
        {
            ProjectDB db = new ProjectDB();

            Assert.AreEqual(true, db.Delete(new Project { ID = 6, Deleted = false}).Deleted);
        }
        #endregion
    }
}
