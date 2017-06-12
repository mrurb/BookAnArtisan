﻿using Model;
using System;
using System.Collections.Generic;
using BLL;
using System.ServiceModel;

namespace WCF
{
	public class ProjectService : IProjectService
	{
		private readonly ProjectController projectController = new ProjectController();
		public Project CreateProject(Project t)
		{
			try
			{
				return projectController.Create(t);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch (Exception)
			{

				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		public Project ReadProject(Project project)
		{
			try
			{
				return projectController.Read(project);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch (Exception)
			{

				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		public Project UpdateProject(Project t)
		{
			try
			{
				return projectController.Update(t);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch (Exception)
			{

				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		public Project DeleteProject(Project t)
		{
			try
			{
				return projectController.Delete(t);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch (Exception)
			{

				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		public List<Project> ReadAllProject()
		{
			try
			{
				return projectController.ReadAll();
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch (Exception)
			{

				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		public List<Project> ReadAllProjectsForUser(User user)
		{
			try
			{
				return projectController.ReadAllForUser(user);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch (Exception)
			{

				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		public Page<Project> ReadProjectPage(int? page, int? pageSize)
		{
			return projectController.ReadProjectPage(page, pageSize);
		}

		public Page<Project> ReadProjectPageForUser(string userId, int? page, int? pageSize)
		{
			return projectController.ReadProjectPage(userId, page, pageSize);
		}
	}
}