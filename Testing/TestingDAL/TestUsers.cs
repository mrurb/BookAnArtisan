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
        private User testUser;
        private UserDB db;

        public TestUsers()
        {
            testUser = new User
            {
                Id = "f93e4146-0ef5-45fb-8088-d1150e91dea4",
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
                Phone = "12345678",
                Address = "Downing Street",
                ApiKey = "apistuff",
                
            };

            db = new UserDB();
        }

        #region DALTesting
        [TestMethod]
        public void TestCreate()
        {
            Assert.IsNotNull(db.Create(testUser));
        }

        [TestMethod]
        public void TestRead()
        {
            Assert.AreEqual(testUser, db.Read(testUser));
        }

        [TestMethod]
        public void TestUpdate()
        {
            testUser.Address = "New street";
            Assert.AreEqual("New street", db.Update(testUser).Address);
        }

        [TestMethod]
        public void TestDelete()
        {
            Assert.AreEqual(testUser, db.Delete(testUser));
        }
        #endregion

        [TestMethod]
        public void TestReadAll()
        {
            Assert.IsTrue(0 < db.ReadAll().Count);
            List<User> list = db.ReadAll();
            foreach (User u in list)
            {
                Assert.IsTrue(u.Id.Length > 0);
            }
        }
    }
}
