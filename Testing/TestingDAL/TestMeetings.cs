using System;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using WCF;

namespace Testing.TestingDAL
{

	[TestClass]
	public class TestMeetings
	{
		private readonly MeetingDb meetingDb = new MeetingDb();
		private MeetingService meetingService;
		private Meeting testMeeting;

		#region setup + teardown
		[ClassInitialize]
		public static void SetUpBeforeClass(TestContext tc)
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
		public static void TearDownAfterClass()
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
		public void SetUp()
		{
			try
			{
				meetingService = new MeetingService();
				testMeeting = new Meeting
				{
					Title = "Generic Title!",
					Id = 1,
					Description = "my descriptions!!",
					StartTime = DateTime.Now,
					EndTime = DateTime.Now,
					CreatedBy = new User { Id = "2083af25-f483-4a02-a62b-71c198147c84" },
					Contact = new User { Id = "2083af25-f483-4a02-a62b-71c198147c84" },
					Deleted = false
				};
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
				meetingService = null;
				testMeeting = null;
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
			meetingService.CreateMeeting(testMeeting);
			var m1 = meetingService.ReadMeeting(testMeeting);
			Assert.AreEqual(testMeeting, m1);
		}

		[TestMethod]
		public void TestUpdateMeeting()
		{
			testMeeting.Title = $"last tested: {DateTime.Now}";
			meetingService.UpdateMeeting(testMeeting);
			var m1 = meetingService.ReadMeeting(testMeeting);
			Assert.AreEqual(testMeeting, m1);
		}

		[TestMethod]
		public void TestReadMeeting()
		{
			var m1 = meetingService.ReadMeeting(testMeeting);
			Assert.AreEqual(testMeeting, m1);
		}

		[TestMethod]
		public void TestDeleteMeeting()
		{
			Assert.AreEqual(meetingService.DeleteMeeting(testMeeting), null);
		}

		[TestMethod]
		public void TestReadAllMeeting()
		{
			var list = meetingService.ReadAllMeeting();
			Assert.IsNotNull(list);
			if (list.Count == 0)
			{
				Assert.IsTrue(false);
			}
			Assert.IsTrue(true); // Success
		}

		[TestMethod]
		public void TestAddUserToMeeting()
		{
			var user = new User
			{
				Id = "f93e4146-0ef5-45fb-8088-d1150e91dea3"
			};
			meetingService.AddUserToMeeting(testMeeting, user);
			var m2 = meetingDb.ReadDetails(testMeeting);
			foreach (var u1 in m2.AppendedUsers)
			{
				if (user.Id != u1.Id)
				{
					Assert.IsTrue(false);
				}
			}
			Assert.IsTrue(true);
		}
	}

}
