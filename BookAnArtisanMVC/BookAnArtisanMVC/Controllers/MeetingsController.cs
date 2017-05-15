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
        private MeetingServiceClient mCl = new MeetingServiceClient();

        // GET: Project
        public ActionResult Index()
        {
            try
            {
                var data = mCl.ReadAllMeeting();
                mCl.Close();
                return View(data);
            }
            catch (Exception e)
            {
                mCl.Abort();
                return new HttpStatusCodeResult(404, e.Message);
            }

        }

        public ActionResult Details(Meeting meeting)
        {
            try
            {
                var data = mCl.ReadMeeting(meeting);
                mCl.Close();
                return View(data);
            }
            catch (Exception)
            {
                mCl.Abort();
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
                mCl.CreateMeeting(meeting);
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
                var data = mCl.ReadMeeting(meeting);
                mCl.Close();
                return View(data);
            }
            catch (Exception)
            {
                mCl.Abort();
                throw;
            }
        }

        // POST: Meeting/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Meeting meeting)
        {
            try
            {
                mCl.UpdateMeeting(meeting);
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
                var data = mCl.ReadMeeting(meeting);
                mCl.Close();
                return View(data);
            }
            catch (Exception)
            {
                mCl.Abort();
                throw;
            }
        }

        // POST: Meeting/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Meeting meeting)
        {
            try
            {
                mCl.DeleteMeeting(meeting);
                return RedirectToAction("MyMeetings");
            }
            catch
            {
                return View(meeting);
            }
        }

        public ActionResult MyMeetings(User user)
        {
            //user.Id = "f93e4146-0ef5-45fb-8088-d1150e91dea3";
            user.Id = HttpContext.User.Identity.GetUserId();
            var data = mCl.ReadAllForUser(user);
            return View(data);
        }
    }
}
