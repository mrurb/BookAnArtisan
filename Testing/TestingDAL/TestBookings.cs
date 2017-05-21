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
				//user1 = uDb.Read(new User { Id = "2083af25-f483-4a02-a62b-71c198147c84" });
				//user2 = uDb.Read(new User { Id = "ef32f29e-1afd-4591-b42c-d0b3838fe6bd" });
				//material = mDb.Read(new Material {Id = 4});
				booking = new Booking()
				{
					StartTime = new DateTime(2014, 2, 15, 12, 00, 00),
					EndTime = new DateTime(2014, 2, 20, 11, 59, 59),
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
				user1 = null;
				user2 = null;
				material = null;

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
			//Assert.IsNotNull(bDb.Create(booking)); check intergration test for inspiration
		}

		[TestMethod]
		public void TestUpdateBooking()
		{
			//blablabla
		}

		[TestMethod]
		public void TestReadBooking()
		{
			//blablabla
		}

		[TestMethod]
		public void TestDeleteBooking()
		{
			//blablabla
		}
		#endregion
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
				bDb.Update(booking); // this should fail. would like to reverse, pls..
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

		[TestMethod]
		public void BoundaryTestBooking2()
		{
			//StartTime = new DateTime(2014, 2, 15, 12, 00, 00),
			//EndTime = new DateTime(2014, 2, 20, 11, 59, 59),
			try
			{
				//Assert.AreEqual(); how to do this assert bitch?
				booking.StartTime = new DateTime(2014, 2, 25, 11, 59, 59);
				bDb.Update(booking); // this should fail. would like to reverse, pls..
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
		#endregion

		#endregion
	}
}
