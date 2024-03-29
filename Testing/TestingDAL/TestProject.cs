﻿using System;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace Testing.TestingDAL
{
	[TestClass]
	public class TestProject
	{
		private static Project testProject;
		private static ProjectDb pDb;

		#region setups + teardowns
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
				// odakdwa
			}
			catch
			{
				throw new Exception();
			}
		}
		[TestInitialize]
		public void SetUp()
		{
			testProject = new Project
			{
					
				Name = "Test",
				CreatedBy = new User { Id = "f93e4146-0ef5-45fb-8088-d1150e91dea3", },
				Contact = new User { Id = "f93e4146-0ef5-45fb-8088-d1150e91dea3" },
				ProjectStatusId = 1,
				ProjectDescription = "Something",
				StreetName = "Test street",
				StartTime = new DateTime(2017, 04, 19, 17, 09, 21, 0),
				Created = new DateTime(2017, 04, 19, 17, 09, 21, 0),
				Modified = new DateTime(2017, 04, 19, 17, 09, 21, 0),
				Deleted = false
			};
			pDb = new ProjectDb();
			testProject = pDb.Create(testProject);
		}
		[TestCleanup]
		public void TearDown()
		{
			try
			{
				pDb.RemoveProject(testProject);
				testProject = null;
				pDb = null;
			}
			catch
			{
				throw new Exception();
			}
		}
		#endregion
		#region CRUDS
		[TestMethod]
		public void TestCreateProject()
		{
			var project = pDb.Create(testProject);
			var project1 = pDb.Read(testProject);
			Assert.AreEqual(project, project1);
			pDb.RemoveProject(project);
		}

		[TestMethod]
		public void TestReadProject()
		{
			Assert.AreEqual(testProject, pDb.Read(testProject));
		}

		[TestMethod]
		public void TestUpdateProject()
		{
			var rand = new Random();
			int genNum = rand.Next(1, 100);
			testProject.StreetName = $"New Street#{genNum}";
			Assert.IsTrue($"New Street#{genNum}".Equals(pDb.Update(testProject).StreetName));
		}

		[TestMethod]
		public void TestDeleteProject()
		{
			pDb.Delete(testProject);
			Assert.IsTrue(pDb.Read(testProject).Deleted);
		}

		[TestMethod]
		public void TestReadAllProject()
		{
			var list = pDb.ReadAll();
			Assert.IsTrue(0 < list.Count);
			foreach (var p in list)
			{
				Assert.IsTrue(p.Id != 0);
			}

		}

		[TestMethod]
		public void TestProjectReadAllForUser()
		{
			var user = new User { Id = "f93e4146-0ef5-45fb-8088-d1150e91dea3" };
			var list = pDb.ReadAllForUser(user);
			Assert.IsTrue(0 < list.Count);
			foreach (var p in list)
			{
				Assert.IsTrue(p.Id != 0);
			}
		}
		#endregion
		#region Boundary Tests

		[TestMethod]
		public void BoundaryTest()
		{
			//no boundaries?
		}

		#endregion
	}
}
