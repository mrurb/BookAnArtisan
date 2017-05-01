using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using WCF;
using Model;
using DAL;

namespace Testing
{

    [TestClass]
    public class MeetingTest
    {
        MeetingService ms;
        Meeting m;
        #region setup + teardown
        [ClassInitialize]
        public static void setUpBeforeClass(TestContext tc)
        {
            try
            {
                //nothing?
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
                //nothing?
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
                ms = new MeetingService();
                m = new Meeting()
                {
                    Title = "Generic Title!",
                    Id = 1,
                    Description = "my descriptions!!",
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now,
                    CreatedById = "2083af25-f483-4a02-a62b-71c198147c84",
                    ContactId = "2083af25-f483-4a02-a62b-71c198147c84",
                    Deleted = false
                };
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
                ms = null;
                m = null;
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        [TestMethod]
        public void TestCreateMeeting()
        {
            ms.Create(m);
            Meeting m1 = ms.Read(m);
            Assert.AreEqual(m, m1);
        }

        [TestMethod]
        public void TestUpdateMeeting()
        {
            m.Title = $"last tested: {DateTime.Now}";
            ms.Update(m);
            Meeting m1 = ms.Read(m);
            Assert.AreEqual(m, m1);
        }

        [TestMethod]
        public void TestReadMeeting()
        {
            Meeting m1 = ms.Read(m);
            Assert.AreEqual(m, m1);
        }

        [TestMethod]
        public void TestDeleteMeeting()
        {
            Assert.AreEqual(ms.Delete(m), null);
        }

        [TestMethod]
        public void TestReadAllMeeting()
        {
            List<Meeting> list = ms.ReadAll();
            Assert.IsNotNull(list);
            if (list.Count == 0)
            {
                Assert.IsTrue(false);
            }
            // Success
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestAddUserToMeeting()
        {
            User u = new User()
            {
                Id = "f93e4146-0ef5-45fb-8088-d1150e91dea3"
            };
            ms.AddUserToMeeting(m, u);
            MeetingDB mdb = new MeetingDB();
            Meeting m2 = mdb.ReadDetails(m);
            foreach (User u1 in m2.AppendedUsers)
            {
                if (!(u.Id == u1.Id))
                {
                    Assert.IsTrue(false);
                }
            }
            Assert.IsTrue(true);
        }
    }

}
