﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using WCF;
using Model;

namespace Testing
{

    [TestClass]
    class MeetingTest
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
                m = new Meeting() {
                    Title = "Generic Title!",
                    Id = 123123123,
                    Description = "my descriptions!!",
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now};
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
            m.Title = "someothertitle";
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
            Assert.IsTrue(ms.Delete(m));
        }

        [TestMethod]
        public void TestReadAllMeeting()
        {
            // TODO
        }
    }
    
}