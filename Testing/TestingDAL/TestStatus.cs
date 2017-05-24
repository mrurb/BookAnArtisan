using System;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace Testing.TestingDAL
{
	[TestClass]
	public class TestStatus
	{
		private static Status testStatus;
		private static StatusDb db;

		[TestMethod]
		public void TestCreateStatus()
		{
			Assert.IsNotNull(db.Create(testStatus));
		}

		[TestMethod]
		public void TestReadStatus()
		{
			Assert.AreEqual(testStatus, db.Read(testStatus));
		}

		[TestMethod]
		public void TestUpdateStatus()
		{
			testStatus.Name = "New name";
			db.Update(testStatus);
			Assert.AreEqual(testStatus.Name, db.Read(testStatus).Name);
		}

		[TestMethod]
		public void TestDeleteStatus()
		{
			Assert.AreEqual(db.Delete(testStatus), null); // fix later TODO
		}

		[TestMethod]
		public void TestReadAllStatus()
		{
			Assert.IsTrue(0 < db.ReadAll().Count);
			var list = db.ReadAll();
			foreach (var s in list)
				Assert.IsTrue(s.Id > 0);
		}

		#region setups + teardowns

		[ClassInitialize]
		public static void SetUpBeforeClass(TestContext tc)
		{
			try
			{
				// nothing?
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
				db = null;
				testStatus = null;
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
				db = new StatusDb();
				testStatus = new Status {Id = 2, Name = "OK"};
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
				db = null;
				testStatus = null;
			}
			catch
			{
				throw new Exception();
			}
		}

		#endregion
	}
}