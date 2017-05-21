using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using WCF;

namespace Testing.TestingWCF
{
	class TestProject
	{
		ProjectService pSv = new ProjectService();
		private Project project;
		#region setups + teardowns
		[ClassInitialize]
		public static void setUpBeforeClass(TestContext tc)
		{
			//do nothing? make local db?
		}
		[ClassCleanup]
		public static void tearDownAfterClass()
		{

		}
		[TestInitialize]
		public void setUp()
		{
			project = new Project()
			{
				Deleted = false,
				Name = "Integration Test Name",
				ProjectDescription = "The description of the integration test",
				StartTime = DateTime.UtcNow,
				StreetName = "Integration Street Name",
				Contact = new User()
				{
					Id = "2083af25-f483-4a02-a62b-71c198147c84",
					Email = "kaw@kaw.kaw",
					FirstName = "Kaw",
					LastName = "Bjorn",
					Address = "Fake Street 123",
					UserName = "kaw@kaw.kaw",
					PhoneNumber = "13374201",
					EmailConfirmed = false
				},
				CreatedBy = new User()
				{
					Email = "stuff@stuff.com",
					EmailConfirmed = false,
					UserName = "stuff@stuff.stuff",
					FirstName = "John",
					LastName = "Doe",
					Address = "New street",
					Id = "f93e4146-0ef5-45fb-8088-d1150e91dea3"
				},
				ProjectStatusID = 1
			};
		}
		[TestCleanup]
		public void tearDown()
		{
		}
		#endregion

		[TestMethod]
		public void ProjectIntegrationTest()
		{
				//create
			pSv.CreateProject(project);
			Project projectdb = pSv.ReadProject(project);
			project.Modified = projectdb.Modified;
			project.Created = projectdb.Created;
			ComparisonProject(project, projectdb);

			//update
			project.Name = "bla";
			project.StartTime = new DateTime(2014, 3, 15, 12, 00, 00);
			project.ProjectDescription = "blablabla";
			project.StreetName = "some street bla";
			project.ProjectStatusID = 2;
			pSv.UpdateProject(project);
			projectdb = pSv.ReadProject(project);
			ComparisonProject(project, projectdb);

			//delete
			project.Deleted = true;
			pSv.DeleteProject(project);
			projectdb = pSv.ReadProject(project);
			Assert.AreEqual(project.Deleted, projectdb.Deleted);
		}

		private void ComparisonProject(Project expected, Project actual)
		{
			//base details
			Assert.AreEqual(expected.Name,actual.Name);
			Assert.AreEqual(expected.StartTime, actual.StartTime);
			Assert.AreEqual(expected.ProjectDescription, actual.ProjectDescription);
			Assert.AreEqual(expected.StreetName, actual.StreetName);
			Assert.AreEqual(expected.ProjectStatusID, actual.ProjectStatusID);
			//contact person details
			Assert.AreEqual(expected.Contact.UserName, actual.Contact.UserName);
			Assert.AreEqual(expected.Contact.FirstName, actual.Contact.FirstName);
			Assert.AreEqual(expected.Contact.LastName, actual.Contact.LastName);
			Assert.AreEqual(expected.Contact.Address, actual.Contact.Address);
			Assert.AreEqual(expected.Contact.Email, actual.Contact.Email);
			Assert.AreEqual(expected.Contact.PhoneNumber, actual.Contact.PhoneNumber);
			Assert.AreEqual(expected.Contact.Id, actual.Contact.Id);
			//created by details
			Assert.AreEqual(expected.CreatedBy.UserName, actual.CreatedBy.UserName);
			Assert.AreEqual(expected.CreatedBy.FirstName, actual.CreatedBy.FirstName);
			Assert.AreEqual(expected.CreatedBy.LastName, actual.CreatedBy.LastName);
			Assert.AreEqual(expected.CreatedBy.Address, actual.CreatedBy.Address);
			Assert.AreEqual(expected.CreatedBy.Email, actual.CreatedBy.Email);
			Assert.AreEqual(expected.CreatedBy.PhoneNumber, actual.CreatedBy.PhoneNumber);
			Assert.AreEqual(expected.CreatedBy.Id, actual.CreatedBy.Id);
		}
	}
}
