using System;
using System.Web.Mvc;
using BookAnArtisanMVC.ServiceReference;
using Microsoft.AspNet.Identity;
using System.ServiceModel;

namespace BookAnArtisanMVC.Controllers
{
	public class ProjectsController : Controller
	{
		private readonly ProjectServiceClient projectServiceClient = new ProjectServiceClient();
		private readonly ProjectSearchClient projectSearchClient = new ProjectSearchClient();

		// GET: Project
		public ActionResult Index()
		{
			try
			{
				var data = projectServiceClient.ReadAllProject();
				projectServiceClient.Close();
				return View(data);
			}
			catch (FaultException e)
			{
				projectServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
			catch (Exception e)
			{
				projectServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
		}

		public ActionResult Details(Project project)
		{
			try
			{
				var data = projectServiceClient.ReadProject(project);
				projectServiceClient.Close();
				return View(data);
			}
			catch (FaultException e)
			{
				projectServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
			catch (Exception e)
			{
				projectServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
		}

		[Authorize]
		public ActionResult Create()
		{
			return View();
		}

		// POST: Project/Create
		[HttpPost]
		[Authorize]
		public ActionResult Create(Project project)
		{
			try
			{
				project.CreatedBy = new User { Id = HttpContext.User.Identity.GetUserId() };
				projectServiceClient.CreateProject(project);
				return RedirectToAction("Index");
			}
			catch (FaultException e)
			{
				projectServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View(project);
			}
			catch (Exception e)
			{
				projectServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View(project);
			}
		}

		// GET: Project/Edit/5
		public ActionResult Edit(Project project)
		{
			try
			{
				var data = projectServiceClient.ReadProject(project);
				projectServiceClient.Close();
				return View(data);
			}
			catch (FaultException e)
			{
				projectServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
			catch (Exception e)
			{
				projectServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
		}

		// POST: Project/Edit/5
		[HttpPost, ActionName("Edit")]
		public ActionResult EditConfirmed(Project project)
		{
			try
			{
				projectServiceClient.UpdateProject(project);
				return RedirectToAction("Index");
			}
			catch (FaultException e)
			{
				projectServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View(project);
			}
			catch (Exception e)
			{
				projectServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View(project);
			}
		}

		// GET: Project/Delete/5
		public ActionResult Delete(Project project)
		{
			try
			{
				var data = projectServiceClient.ReadProject(project);
				projectServiceClient.Close();
				return View(data);
			}
			catch (FaultException e)
			{
				projectServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
			catch (Exception e)
			{
				projectServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
		}

		// POST: Project/Delete/5
		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(Project project)
		{
			try
			{
				projectServiceClient.DeleteProject(project);
				return RedirectToAction("Index");
			}
			catch (FaultException e)
			{
				projectServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
			catch (Exception e)
			{
				projectServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
		}

		public ActionResult ProjectSearch()
		{
			var p = new Project { Name = "a" };
			var data = projectSearchClient.SearchByProjectAddress(p);
			return View(data);
			//return View(projectServiceClient.ReadAllProject());
		}

		[HttpPost, ActionName("ProjectSearch")]
		public ActionResult ProjectSearch(Project projects)
		{
			var p = new Project { Name = "a" };
			var data = projectSearchClient.SearchByProjectAddress(p);
			return View(data);
		}

		public ActionResult MyProjects(User user)
		{
			try
			{
				user.Id = HttpContext.User.Identity.GetUserId();
				var data = projectServiceClient.ReadAllProjectsForUser(user);
				return View(data);
			}
			catch (FaultException e)
			{
				projectServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
			catch (Exception e)
			{
				projectServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
		}
	}
}
