using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookAnArtisanMVC.ServiceReference;
using Microsoft.AspNet.Identity;

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
                var data = pCl.ReadAllMeeting();
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
                var data = pCl.ReadMeeting(meeting);
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
                meeting.CreatedBy.Id = User.Identity.GetUserId();
                pCl.CreateMeeting(meeting);
                return RedirectToAction("MyMeetings");
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
                var data = pCl.ReadMeeting(meeting);
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
                pCl.UpdateMeeting(meeting);
                return RedirectToAction("MyMeetings"); 
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
                var data = pCl.ReadMeeting(meeting);
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
                pCl.DeleteMeeting(meeting);
                return RedirectToAction("MyMeetings");
            }
            catch
            {
                return View(meeting);
            }
        }

        public ActionResult MyMeetings(User user)
        {
            user.Id = "2083af25-f483-4a02-a62b-71c198147c84";
            var data = pCl.ReadAllForUser(user);
            return View(data);
        }
    }
}
