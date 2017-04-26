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
            // do nothing?
        }
        #region setups and teardowns
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
                testRole = null;
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
                testRole = new Role { Id = "1234", Name = "Admin" };
                db = new RoleDB();
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
                testRole = null;
                db = null;
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion
        [TestMethod]
        public void TestCreateRole()
        {
            Assert.IsNotNull(db.Create(testRole));
        }

        [TestMethod]
        public void TestReadRole()
        {
            Assert.AreEqual(testRole, db.Read(testRole));
        }

        [TestMethod]
        public void TestUpdateRole()
        {
            testRole.Name = "User";
            Assert.AreEqual("User", db.Update(testRole).Name);
        }

        [TestMethod]
        public void TestDeleteRole()
        {
            Assert.AreEqual(testRole, db.Delete(testRole));
        }

        [TestMethod]
        public void TestReadAllRole()
        {
            List<Role> list = db.ReadAll();
            if (list.Count > 0)
            {
                foreach (Role r in list)
                {
                    if (r.Id.Length == 0)
                    {
                        Assert.IsTrue(false); //if we get here, the test fails
                    }
                }
                Assert.IsTrue(true); //if we get here, the test succeeded
            }
            else
            {
                Assert.IsTrue(false); //if we get here, the test fails
            }
        }
    }
}
