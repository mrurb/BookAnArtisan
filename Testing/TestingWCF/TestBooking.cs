using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using WCF;
using System.ServiceModel;

namespace Testing.TestingWCF
{
	class TestBooking
	{
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

		}
		[TestCleanup]
		public void tearDown()
		{
		}
		#endregion
		RentingService rs = new RentingService();

		[TestMethod]
		public void BookingIntegrationTest()
		{
			Booking bookingnew = new Booking()
			{
				StartTime = new DateTime(2014, 2, 15, 12, 00, 00),
				EndTime = new DateTime(2014, 2, 20, 11, 59, 59),
				Created = DateTime.Now,
				Deleted = false,
				Updated = DateTime.Now,
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
			try
			{
				rs.CreateBooking(bookingnew);
				Assert.AreEqual(bookingnew, rs.ReadBooking(bookingnew)); //one for each field or read operation? //object reference comparison?

				rs.ReadBooking(bookingnew); // what if read doesnt work????
				Assert.AreEqual(bookingnew, rs.ReadBooking(bookingnew)); //one for each field?? to make sure it works?? but what if create doesnt work???

				bookingnew.Deleted = true; // delete locally
				bookingnew.EndTime = new DateTime(2014, 2, 21, 12, 00, 00); // update endtime locally
				rs.UpdateBooking(bookingnew); //update endtime in DB
				Assert.AreEqual(bookingnew, rs.ReadBooking(bookingnew)); //one for each field or read operation? //object reference comparison?

				rs.DeleteBooking(bookingnew); // delete in DB
				Assert.AreEqual(bookingnew.Deleted, rs.ReadBooking(bookingnew).Deleted); //compare deleted.
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
	}
}