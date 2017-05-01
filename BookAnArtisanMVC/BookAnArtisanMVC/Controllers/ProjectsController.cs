using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookAnArtisanMVC.ProjectServiceReference;

namespace BookAnArtisanMVC.Controllers
{
    public class ProjectsController : Controller
    {
        ProjectServiceClient pCl = new ProjectServiceClient();

        // GET: Project
        public ActionResult Index()
        {
            try
            {
                var data = pCl.ReadAll();
                pCl.Close();
                return View(data);
            }
            catch (Exception)
            {
                pCl.Abort();
                return View("NoResponseFromServer");
                //return new HttpStatusCodeResult(404, "Item Not Found");
            }
            
        }

        public ActionResult Details(Project project)
        {
            try
            {
                var data = pCl.Read(project);
                pCl.Close();
                return View(data);
            }
            catch (Exception)
            {
                pCl.Abort();
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
                pCl.Create(project);
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
                var data = pCl.Read(project);
                pCl.Close();
                return View(data);
            }
            catch (Exception)
            {
                pCl.Abort();
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
                pCl.Update(project);
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
                var data = pCl.Delete(project);
                pCl.Close();
                return View(data);
            }
            catch (Exception)
            {
                pCl.Abort();
                throw;
            }
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Project project)
        {
            try
            {
                pCl.Delete(project);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(project);
            }
        }
    }
}
