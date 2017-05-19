using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookAnArtisanMVC.ServiceReference;
using Microsoft.AspNet.Identity;
using System.ServiceModel;

namespace BookAnArtisanMVC.Controllers
{
    public class ProjectsController : Controller
    {
        ProjectServiceClient _pCl = new ProjectServiceClient();
        ProjectSearchClient _psCl = new ProjectSearchClient();
        // GET: Project
        public ActionResult Index()
        {
            try
            {
                var data = _pCl.ReadAllProject();
                _pCl.Close();
                return View(data);
            }
            catch (FaultException e)
            {
                _pCl.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
            catch (Exception e)
            {
                _pCl.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
        }

        public ActionResult Details(Project project)
        {
            try
            {
                var data = _pCl.ReadProject(project);
                _pCl.Close();
                return View(data);
            }
            catch (FaultException e)
            {
                _pCl.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
            catch (Exception e)
            {
                _pCl.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(Project project)
        {
            try
            {
                project.CreatedBy = new User { Id = HttpContext.User.Identity.GetUserId() };
                _pCl.CreateProject(project);
                return RedirectToAction("Index");
            }
            catch (FaultException e)
            {
                _pCl.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View(project);
            }
            catch (Exception e)
            {
                _pCl.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View(project);
            }
        }

        // GET: Project/Edit/5
        public ActionResult Edit(Project project)
        {
            try
            {
                var data = _pCl.ReadProject(project);
                _pCl.Close();
                return View(data);
            }
            catch (FaultException e)
            {
                _pCl.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
            catch (Exception e)
            {
                _pCl.Abort();
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
                _pCl.UpdateProject(project);
                return RedirectToAction("Index");
            }
            catch (FaultException e)
            {
                _pCl.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View(project);
            }
            catch (Exception e)
            {
                _pCl.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View(project);
            }
        }

        // GET: Project/Delete/5
        public ActionResult Delete(Project project)
        {
            try
            {
                var data = _pCl.ReadProject(project);
                _pCl.Close();
                return View(data);
            }
            catch (FaultException e)
            {
                _pCl.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
            catch (Exception e)
            {
                _pCl.Abort();
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
                _pCl.DeleteProject(project);
                return RedirectToAction("Index");
            }
            catch (FaultException e)
            {
                _pCl.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
            catch (Exception e)
            {
                _pCl.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
        }

        public ActionResult ProjectSearch()
        {
            Project p = new Project();
            p.Name = "a";
            var data = _psCl.SearchByProjectAddress(p);
            return View(data);
            //return View(pCl.ReadAllProject());
        }

        [HttpPost, ActionName("ProjectSearch")]
        public ActionResult ProjectSearch(Project projects)
        {
            Project p = new Project();
            p.Name = "a";
            var data = _psCl.SearchByProjectAddress(p);
            return View(data);
        }

        public ActionResult MyProjects(User user)
        {
            try
            {
                user.Id = HttpContext.User.Identity.GetUserId();
                var data = _pCl.ReadAllProjectsForUser(user);
                return View(data);
            }
            catch (FaultException e)
            {
                _pCl.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
            catch (Exception e)
            {
                _pCl.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
        }
    }
}
