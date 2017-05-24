using System;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using WCF;

namespace Testing.TestingDAL
{
	[TestClass]
	public class TestUsers
	{
		private static User testUser;
		private static UserDb userDb;
		private static MeetingService meetingService;

		[TestMethod]
		public void TestReadAllUsers()
		{
			var list = userDb.ReadAll();
			if (list.Count > 0)
			{
				foreach (var u in list)
					if (u.Id.Length == 0)
						Assert.IsTrue(false); //if we get here, the test fails
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
			var list = meetingService.ReadAllForUser(testUser);
			Assert.IsNotNull(list);
			if (list.Count == 0)
				Assert.IsTrue(false);
			// Success
			Assert.IsTrue(true);
		}

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
		public static void TearDownAfterClass()
		{
			try
			{
				userDb = null;
				testUser = null;
				meetingService = null;
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
					ApiKey = "apistuff"
				};
				userDb = new UserDb();
				meetingService = new MeetingService();
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
				userDb = null;
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
			Assert.IsNotNull(userDb.Create(testUser));
		}

		[TestMethod]
		public void TestReadUsers()
		{
			Assert.AreEqual(testUser, userDb.Read(testUser));
		}

		[TestMethod]
		public void TestUpdateUsers()
		{
			testUser.Address = "New street";
			userDb.Update(testUser);
			Assert.AreEqual(testUser.Address, userDb.Read(testUser).Address);
		}

		[TestMethod]
		public void TestDeleteUsers()
		{
			Assert.AreEqual(testUser, userDb.Delete(testUser));
		}

		#endregion
	}
}