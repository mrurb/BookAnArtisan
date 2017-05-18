using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using DAL;
using WCF;

namespace Testing.TestingDAL
{
	[TestClass]
	public class TestBookings
	{
		private static User _user1;
		private static Material _material;
		private static Booking _booking;
		private static UserDB _uDb;
		private static MaterialDB _mDb;
		private static RentingDb _bDb;

		#region SetUp and TearDowns
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
				_booking = null;
				_bDb = null;
				_uDb = null;
				_mDb = null;
				_user1 = null;
				_material = null;
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
				_uDb = new UserDB();
				_mDb = new MaterialDB();
				_bDb = new RentingDb();
				new RentingService();
				_user1 = _uDb.Read(new User { Id = "2083af25-f483-4a02-a62b-71c198147c84" });
				_uDb.Read(new User { Id = "ef32f29e-1afd-4591-b42c-d0b3838fe6bd" });
				_material = _mDb.Read(new Material { Id = 4 });
				_booking = new Booking
				{
					Id = 95035,
					Created = DateTime.UtcNow,
					Deleted = false,
					StartTime = Convert.ToDateTime("2023-11-12 02:05:00.000"),
					EndTime = Convert.ToDateTime("2023-11-12 03:00:00.000"),
					Item = _material,
					Updated = DateTime.UtcNow,
					User = _user1
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
				_booking = null;
				_uDb = null;
				_bDb = null;
				_mDb = null;
				_user1 = null;
				_material = null;

			}
			catch
			{
				throw new Exception();
			}
		}
		#endregion

		#region  Test Methods

		[TestMethod]
		public void TestCreateBooking()
		{
			Assert.IsNotNull(_bDb.Create(_booking));
		}

		[TestMethod]
		public void TestReadBooking()
		{
			var booking = _bDb.Read(new Booking { Id = 1 });
			Assert.IsTrue(booking.User.Id != null);
		}

		[TestMethod]
		public void TestReadAllBookings()
		{
			var list = _bDb.ReadAll();
			foreach (var booking in list)
			{
				Assert.IsTrue(booking.User.Id != null);
			}
		}


		[TestMethod]
		public void TestUpdateBooking()
		{
			var booking = _bDb.Read(new Booking { Id = 1 });
			var oldUpdated = booking.Updated;
			booking.StartTime = Convert.ToDateTime("2016-05-08 02:00:00.000");
			booking.EndTime = Convert.ToDateTime("2016-05-10 02:00:00.000");
			booking.Updated = DateTime.UtcNow;

			_bDb.Update(booking); // do the update

			var booking2 = _bDb.Read(new Booking { Id = 1 }); // read updated entry
			Assert.IsTrue(booking2.Updated != oldUpdated); // is different = success
		}


		#endregion
	}
}
