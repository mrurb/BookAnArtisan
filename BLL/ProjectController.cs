using System;
using System.Collections.Generic;
using Model;
using DAL;

namespace BLL
{
	public class ProjectController : IController<Project>
	{
		private readonly ProjectDb projectDb = new ProjectDb();
		private readonly SearchDb searchDb = new SearchDb();
		private readonly UserController uctr = new UserController();
		public Project Create(Project project)
		{
			var a = uctr.Read(project.Contact);
			if (a.UserName != project.Contact.UserName)
			{
				throw new ApplicationException("User: Contact invalid.");
			}
			var b = uctr.Read(project.CreatedBy);
			if (b.UserName != project.CreatedBy.UserName)
			{
				throw new ApplicationException("User: CreatedBy invalid."); 
			}
			projectDb.Create(project);
			return projectDb.Read(project);
		}

		public Project Read(Project project)
		{
			return projectDb.Read(project);
		}

		public Project Update(Project project)
		{
			return projectDb.Update(project);
		}

		public Project Delete(Project project)
		{
			return projectDb.Delete(project);
		}

		public List<Project> ReadAll()
		{
			return projectDb.ReadAll();
		}

		public List<Project> ReadAllForUser(User user)
		{
			if (user == null)
			{
				throw new ApplicationException("No user selected.");
			}
			return projectDb.ReadAllForUser(user);
		}

		public List<Project> SearchByProjectAddress(Project p)
		{
			return searchDb.SearchByProjectAddress(p);
		}

		public Page<Project> ReadProjectPage(int? page, int? pageSize)
		{
			return projectDb.ReadProjectPage(page, pageSize);
		}

		public Page<Project> ReadProjectPage(string userId, int? page, int? pageSize)
		{
			return projectDb.ReadProjectPage(userId, page, pageSize);
		}
	}
}

