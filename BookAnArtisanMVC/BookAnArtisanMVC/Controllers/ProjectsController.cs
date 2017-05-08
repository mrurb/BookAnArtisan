using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookAnArtisanMVC.ServiceReference;

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
            catch (Exception)
            {
                _pCl.Abort();
                return View("NoResponseFromServer");
                //return new HttpStatusCodeResult(404, "Item Not Found");
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
            catch (Exception)
            {
                _pCl.Abort();
                throw;
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
                // TODO: Add insert logic here
                _pCl.CreateProject(project);
                return RedirectToAction("Index");
            }
            catch
            {
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
            catch (Exception)
            {
                _pCl.Abort();
                throw;
            }
        }

        // POST: Project/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Project project)
        {
            try
            {
                // TODO: Add update logic here
                _pCl.UpdateProject(project);
                return RedirectToAction("Index");
            }
            catch
            {
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
            catch (Exception)
            {
                _pCl.Abort();
                throw;
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
            catch
            {
                return View(project);
            }
        }

        public ActionResult ProjectSearch()
        {
            var data = _psCl.SearchByProjectAddress("a");
            return View(data);
            //return View(pCl.ReadAllProject());
        }

        [HttpPost, ActionName("ProjectSearch")]
        public ActionResult ProjectSearch(Project projects) // maybe not list ... 
        {
            var data = _psCl.SearchByProjectAddress("a");
            return View(data);
        }

        public ActionResult MyProjects(User user)
        {
            user.Id = "2083af25-f483-4a02-a62b-71c198147c84";
            var data = _pCl.ReadAllProjectsForUser(user);
            return View(data);
        }
    }
}
