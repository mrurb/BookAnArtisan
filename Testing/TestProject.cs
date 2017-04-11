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
                Name = "Test project",
                Created_by_ID = "1",
                Contact_ID = "2",
                Project_status_ID = 1,
                Project_description = "Something",
                Street_Name = "Vej 1, by 2",
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
        }
        #endregion
    }
}
