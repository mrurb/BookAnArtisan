using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using DAL;
using System.Collections.Generic;

namespace Testing.TestingDAL
{
    [TestClass]
    public class TestUsers
    {
        static private User testUser;
        static private UserDB db;

        public TestUsers()
        {
            //do nothing?
        }

        #region setups and teardowns
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
        public static  void tearDownAfterClass()
        {
            try
            {
                db = null;
                testUser = null;
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
                testUser = new User
                {
                    Id = "f93e4146-0ef5-45fb-8088-d1150e91dea3",
                    Email = "stuff@stuff.com",
                    EmailConfirmed = true,
                    PasswordHash = "badpasswordhash",
                    SecurityStamp = "stamp",
                    PhoneNumber = "87654321",
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEndDateUtc = DateTime.Today,
                    LockoutEnabled = false,
                    AccessFailedCount = 3,
                    UserName = "pwnMaster",
                    FirstName = "John",
                    LastName = "Doe",
                    Address = "Downing Street",
                    ApiKey = "apistuff",
                };
                db = new UserDB();
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
                testUser = null;
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion
        #region DALTesting
        [TestMethod]
        public void TestCreateUsers()
        {
            Assert.IsNotNull(db.Create(testUser));
        }

        [TestMethod]
        public void TestReadUsers()
        {
            Assert.AreEqual(testUser, db.Read(testUser));
        }

        [TestMethod]
        public void TestUpdateUsers()
        {
            testUser.Address = "New street";
            db.Update(testUser);
            Assert.AreEqual(testUser.Address, db.Read(testUser).Address);
        }

        [TestMethod]
        public void TestDeleteUsers()
        {
            Assert.AreEqual(testUser, db.Delete(testUser));
        }
        #endregion

        [TestMethod]
        public void TestReadAllUsers()
        {
            List<User> list = db.ReadAll();
            if (list.Count > 0)
            {
                foreach (User u in list)
                {
                    if (u.Id.Length == 0)
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
