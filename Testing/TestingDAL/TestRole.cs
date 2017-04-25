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
    public class TestRole
    {
        private Role testRole;
        private RoleDB db;

        public TestRole()
        {
            testRole = new Role { Id = "1234", Name = "Admin" };
            db = new RoleDB();
        }

        [TestMethod]
        public void TestCreate()
        {
            Assert.IsNotNull(db.Create(testRole));
        }

        [TestMethod]
        public void TestRead()
        {
            Assert.AreEqual(testRole, db.Read(testRole));
        }

        [TestMethod]
        public void TestUpdate()
        {
            testRole.Name = "User";
            Assert.AreEqual("User", db.Update(testRole).Name);
        }

        [TestMethod]
        public void TestDelete()
        {
            Assert.AreEqual(testRole, db.Delete(testRole));
        }

        [TestMethod]
        public void TestReadAll()
        {
            Assert.IsTrue(0 < db.ReadAll().Count);
            List<Role> list = db.ReadAll();
            foreach (Role r in list)
            {
                Assert.IsTrue(r.Id.Length > 0);
            }
        }
    }
}
