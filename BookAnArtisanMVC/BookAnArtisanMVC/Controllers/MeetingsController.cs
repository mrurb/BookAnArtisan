using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookAnArtisanMVC.MeetingServiceReference;

namespace BookAnArtisanMVC.Controllers
{
    public class MeetingsController : Controller
    {
        MeetingServiceClient pCl = new MeetingServiceClient();

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
                return new HttpStatusCodeResult(404, "Item Not Found");
            }

        }

        public ActionResult Details(Meeting meeting)
        {
            try
            {
                var data = pCl.Read(meeting); //brug ReadDetails function istedet
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
        public ActionResult Create(Meeting meeting)
        {
            try
            {
                // TODO: Add insert logic here
                pCl.Create(meeting);
                return RedirectToAction("Index"); //redirect to ReadDetails istedet??? TODO
            }
            catch
            {
                return View(meeting);
            }
        }

        // GET: Project/Edit/5
        public ActionResult Edit(Meeting meeting)
        {
            try
            {
                var data = pCl.Read(meeting);
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
        public ActionResult EditConfirmed(Meeting meeting)
        {
            try
            {
                // TODO: Add update logic here
                pCl.Update(meeting);
                return RedirectToAction("Index"); // TODO send bruger til Read istedet???
            }
            catch
            {
                return View(meeting);
            }
        }

        // GET: Project/Delete/5
        public ActionResult Delete(Meeting meeting)
        {
            try
            {
                var data = pCl.Delete(meeting);
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
        public ActionResult DeleteConfirmed(Meeting meeting)
        {
            try
            {
                // TODO: Add delete logic here
                pCl.Delete(meeting);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(meeting);
            }
        }
    }
}
