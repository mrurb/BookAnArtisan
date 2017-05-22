using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using WCF;
using System.ServiceModel;
using DAL;

namespace Testing.TestingWCF
{
	class TestBooking
	{
		private Booking bookingnew;
		private BookingDb bDb;
		#region setups + teardowns
		[ClassInitialize]
		public static void setUpBeforeClass(TestContext tc)
		{
			//do nothing
		}
		[ClassCleanup]
		public static void tearDownAfterClass()
		{

		}
		[TestInitialize]
		public void setUp()
		{
			Booking bookingnew = new Booking()
			{
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
			bDb = new BookingDb();
			bookingnew.Id = rs.CreateBooking(bookingnew).Id;
		}
		[TestCleanup]
		public void tearDown()
		{
			bDb.RemoveBooking(bookingnew);
			bDb = null;
			bookingnew = null;
		}
		#endregion
		BookingService rs = new BookingService();

		[TestMethod]
		public void BookingIntegrationTest()
		{
			
			
			try
			{
				//test create
				Booking bookingnewa = rs.CreateBooking(bookingnew);
				Booking dbBooking = rs.ReadBooking(bookingnewa);
				ComparisonBooking(bookingnewa, dbBooking);
				bDb.RemoveBooking(bookingnewa);
				//this also tests read.

				//test update 
				bookingnew.StartTime = new DateTime(2014, 2, 13, 12, 00, 00);		//update starttime locally
				bookingnew.EndTime = new DateTime(2014, 2, 21, 12, 00, 00);			//update endtime locally
				rs.UpdateBooking(bookingnew);										//update in DB
				dbBooking = rs.ReadBooking(bookingnew);
				Assert.AreEqual(bookingnew.StartTime, dbBooking.StartTime);			//compare.
				Assert.AreEqual(bookingnew.EndTime, dbBooking.EndTime);				//compare.

						//test delete
				bookingnew.Deleted = true;											//delete locally
				rs.DeleteBooking(bookingnew);										//delete in DB
				Assert.AreEqual(bookingnew.Deleted, rs.ReadBooking(bookingnew).Deleted); //compare.
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch (Exception ex)
			{
				//log(ex);
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

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
	}
}