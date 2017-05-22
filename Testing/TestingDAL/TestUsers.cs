using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using DAL;
using WCF;

namespace Testing.TestingDAL
{
    [TestClass]
    public class TestUsers
    {
        private static User _testUser;
        private static UserDB _db;
        private static MeetingService _ms;

        #region setups and teardowns
        [ClassInitialize]
        public static void SetUpBeforeClass(TestContext tc)
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
        public static  void TearDownAfterClass()
        {
            try
            {
                _db = null;
                _testUser = null;
                _ms = null;
            }
            catch
            {
                throw new Exception();
            }
        }

        [TestInitialize]
        public void SetUp()
        {
            try
            {
                _testUser = new User
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
                _db = new UserDB();
                _ms = new MeetingService();
            }
            catch
            {
                throw new Exception();
            }
        }

        [TestCleanup]
        public void TearDown()
        {
            try
            {
                _db = null;
                _testUser = null;

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
            Assert.IsNotNull(_db.Create(_testUser));
        }

        [TestMethod]
        public void TestReadUsers()
        {
            Assert.AreEqual(_testUser, _db.Read(_testUser));
        }

        [TestMethod]
        public void TestUpdateUsers()
        {
            _testUser.Address = "New street";
            _db.Update(_testUser);
            Assert.AreEqual(_testUser.Address, _db.Read(_testUser).Address);
        }

        [TestMethod]
        public void TestDeleteUsers()
        {
            Assert.AreEqual(_testUser, _db.Delete(_testUser));
        }
        #endregion

        [TestMethod]
        public void TestReadAllUsers()
        {
            var list = _db.ReadAll();
            if (list.Count > 0)
            {
                foreach (var u in list)
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

        [TestMethod]
        public void TestReadAllMeetingsForUser()
        {

            var list = _ms.ReadAllForUser(_testUser);
            Assert.IsNotNull(list);
            if (list.Count == 0)
            {
                Assert.IsTrue(false);
            }
            // Success
            Assert.IsTrue(true);
        }
    }
}
