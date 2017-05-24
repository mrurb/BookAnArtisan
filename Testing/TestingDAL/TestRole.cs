using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;

namespace Testing.TestingDAL
{
	[TestClass]
	public class TestRole
	{
		private static Role testRole;
		private static UserRoleDb db;

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
				testRole = null;
				db = null;
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
				testRole = new Role { Id = "1234", Name = "Admin" };
				db = new UserRoleDb();
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
			db.Update(testRole);
			Assert.AreEqual(testRole.Name, db.Read(testRole).Name);
		}

		[TestMethod]
		public void TestDeleteRole()
		{
			Assert.AreEqual(db.Delete(testRole), null);
		}

		[TestMethod]
		public void TestReadAllRole()
		{
			var list = db.ReadAll();
			if (list.Count > 0)
			{
				foreach (var r in list)
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
