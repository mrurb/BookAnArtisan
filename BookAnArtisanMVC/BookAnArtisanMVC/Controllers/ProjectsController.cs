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
        //ProjectServiceClient pCl = new ProjectServiceClient();

        // GET: Project
        public ActionResult Index()
        {
            ProjectServiceClient pCl = new ProjectServiceClient();
            try
            {
                var data = pCl.ReadAll();
                pCl.Close();
                return View(data);
            }
            catch (Exception)
            {
                pCl.Abort();
                throw;
            }
            
        }

        public ActionResult Details(Project project)
        {
            ProjectServiceClient pCl = new ProjectServiceClient();
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
            ProjectServiceClient pCl = new ProjectServiceClient();
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
            ProjectServiceClient pCl = new ProjectServiceClient();
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
            ProjectServiceClient pCl = new ProjectServiceClient();
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
            ProjectServiceClient pCl = new ProjectServiceClient();
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
            ProjectServiceClient pCl = new ProjectServiceClient();
            try
            {
                // TODO: Add delete logic here
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
