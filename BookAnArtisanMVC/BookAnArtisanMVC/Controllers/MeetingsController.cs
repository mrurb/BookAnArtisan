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
            catch (Exception e)
            {
                pCl.Abort();
                return new HttpStatusCodeResult(404, e.Message);
            }

        }

        public ActionResult Details(Meeting meeting)
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

        public ActionResult Create()
        {
            return View();
        }

        // POST: Meeting/Create
        [HttpPost]
        public ActionResult Create(Meeting meeting)
        {
            try
            {
                // TODO: Add insert logic here
                meeting.CreatedById = "2083af25-f483-4a02-a62b-71c198147c84";
                pCl.Create(meeting);
                return RedirectToAction("Index"); //redirect to Read istedet??? TODO
            }
            catch
            {
                return View(meeting);
            }
        }

        // GET: Meeting/Edit/5
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

        // POST: Meeting/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Meeting meeting)
        {
            try
            {
                pCl.Update(meeting);
                return RedirectToAction("Index"); 
            }
            catch
            {
                return View(meeting);
            }
        }

        // GET: Meeting/Delete/5
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

        // POST: Meeting/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Meeting meeting)
        {
            try
            {
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
