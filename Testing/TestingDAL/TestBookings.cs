using System;
using System.ServiceModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using DAL;
using WCF;

namespace Testing.TestingDAL
{
	[TestClass]
	class TestBookings
	{
		private static User user1;
		private static User user2;
		private static Material material;
		private static Booking booking;
		private static UserDB uDb;
		private static MaterialDB mDb;
		private static RentingDb bDb;

		#region SetUp and TearDowns
		[ClassInitialize]
		public static void SetUpBeforeClass(TestContext tc)
		{
			//nothing, I guess? maybe local db?
		}

		[ClassCleanup]
		public static void TearDownAfterClass()
		{
			//nothing, I guess? maybe local db?
		}

		[TestInitialize]
		public void SetUp()
		{
			try
			{
				uDb = new UserDB();
				mDb = new MaterialDB();
				bDb = new RentingDb();
				Booking bookingnew = new Booking()
				{
					Id = 2,
					StartTime = new DateTime(2017, 5, 25, 10, 50, 00),
					EndTime = new DateTime(2017, 5, 28, 10, 10, 00),
					//Created = DateTime.Now, // I don't control this
					Deleted = false,
					//Updated = DateTime.Now, // I don't control this. (should be nowutc)
					Item = new Material()
					{
						Name = "Traktor",
						Description = "En rød traktor med hjul",
						Condition = "Virker fint. Ryger lidt.",
						Deleted = false,
						Available = true,
						Id = 1,
						Owner = new User()
						{
							Id = "2083af25-f483-4a02-a62b-71c198147c84",
							Email = "kaw@kaw.kaw",
							FirstName = "Kaw",
							LastName = "Bjorn",
							Address = "Fake Street 123",
							UserName = "kaw@kaw.kaw",
							PhoneNumber = "13374201",
							EmailConfirmed = false
						}
					},
					User = new User()
					{
						Email = "stuff@stuff.com",
						EmailConfirmed = false,
						UserName = "stuff@stuff.stuff",
						FirstName = "John",
						LastName = "Doe",
						Address = "New street",
						Id = "f93e4146-0ef5-45fb-8088-d1150e91dea3"
					}
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
				booking = null;
				uDb = null;
				bDb = null;
				mDb = null;
			}
			catch
			{
				throw new Exception();
			}
		}
		#endregion

		#region  Test Methods


		#region CRUD tests
		
		[TestMethod]
		public void TestCreateBooking()
		{
			bDb.Create(booking); //can't, already exists...
			Booking dbBooking = bDb.Read(booking);
			ComparisonBooking(booking,dbBooking);
		}

		[TestMethod]
		public void TestUpdateBooking()
		{
			booking.StartTime = new DateTime(2012, 1, 1, 12, 00, 00);
			booking.EndTime = new DateTime(2014, 8, 28, 23, 59, 00);
			bDb.Update(booking);
			Booking dBooking = bDb.Read(booking);
			ComparisonBooking(booking, dBooking);
		}

		[TestMethod]
		public void TestReadBooking()
		{
			Booking dBooking = bDb.Read(booking);
			ComparisonBooking(booking, dBooking);
		}

		[TestMethod]
		public void TestDeleteBooking()
		{
			booking.Deleted = true;
			bDb.Delete(booking); // needs id..
			Booking dBooking = bDb.Read(booking);
			Assert.AreEqual(booking.Deleted, dBooking.Deleted);
		}
		#endregion
		private void ComparisonBooking(Booking expected, Booking actual)
		{
			//base details of booking
			Assert.AreEqual(expected.Deleted, actual.Deleted);
			Assert.AreEqual(expected.StartTime, actual.StartTime);
			Assert.AreEqual(expected.EndTime, actual.EndTime);
			//Assert.AreEqual(expected.Created, actual.Created);
			//Assert.AreEqual(expected.Updated, actual.Updated);
			//base details of item
			Assert.AreEqual(expected.Item.Id, actual.Item.Id);
			Assert.AreEqual(expected.Item.Name, actual.Item.Name);
			Assert.AreEqual(expected.Item.Deleted, actual.Item.Deleted);
			Assert.AreEqual(expected.Item.Available, actual.Item.Available);
			Assert.AreEqual(expected.Item.Condition, actual.Item.Condition);
			Assert.AreEqual(expected.Item.Description, actual.Item.Description);
			//owner of item
			Assert.AreEqual(expected.Item.Owner.UserName, actual.Item.Owner.UserName);
			Assert.AreEqual(expected.Item.Owner.Email, actual.Item.Owner.Email);
			Assert.AreEqual(expected.Item.Owner.Address, actual.Item.Owner.Address);
			Assert.AreEqual(expected.Item.Owner.FirstName, actual.Item.Owner.FirstName);
			Assert.AreEqual(expected.Item.Owner.LastName, actual.Item.Owner.LastName);
			Assert.AreEqual(expected.Item.Owner.Id, actual.Item.Owner.Id);
			Assert.AreEqual(expected.Item.Owner.PhoneNumber, actual.Item.Owner.PhoneNumber);
			//the renter
			Assert.AreEqual(expected.User.UserName, actual.User.UserName);
			Assert.AreEqual(expected.User.Email, actual.User.Email);
			Assert.AreEqual(expected.User.Address, actual.User.Address);
			Assert.AreEqual(expected.User.FirstName, actual.User.FirstName);
			Assert.AreEqual(expected.User.LastName, actual.User.LastName);
			Assert.AreEqual(expected.User.Id, actual.User.Id);
			Assert.AreEqual(expected.User.PhoneNumber, actual.User.PhoneNumber);
		}

		#region BoundaryTests

		[TestMethod]
		public void BoundaryTestBooking()
		{
			//StartTime = new DateTime(2014, 2, 15, 12, 00, 00),
			//EndTime = new DateTime(2014, 2, 20, 11, 59, 59),
			try
			{
				//Assert.AreEqual(); how to do this assert bitch?
				booking.EndTime = new DateTime(2014, 2, 15, 12, 00, 00);
				bDb.Update(booking);
				Assert.IsFalse(true);
			}
			catch (ApplicationException ex)
			{
				Assert.IsFalse(false);
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch (Exception ex)
			{
				//log(ex);
				Assert.IsFalse(false);
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		[TestMethod]
		public void BoundaryTestBooking2()
		{
			//StartTime = new DateTime(2014, 2, 15, 12, 00, 00),
			//EndTime = new DateTime(2014, 2, 20, 11, 59, 59),
			try
			{
				//Assert.AreEqual(); how to do this assert bitch?
				booking.StartTime = new DateTime(2014, 2, 25, 11, 59, 59);
				bDb.Update(booking);
				Assert.IsFalse(true);
			}
			catch (ApplicationException ex)
			{
				Assert.IsFalse(false);
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch (Exception ex)
			{
				//log(ex);
				Assert.IsFalse(false);
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}
		#endregion

		#endregion
	}
}
