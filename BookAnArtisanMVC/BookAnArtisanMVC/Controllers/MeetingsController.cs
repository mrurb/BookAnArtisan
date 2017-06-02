using System;
using System.ServiceModel;
using System.Web.Mvc;
using BookAnArtisanMVC.Models;
using BookAnArtisanMVC.ServiceReference;
using Microsoft.AspNet.Identity;

namespace BookAnArtisanMVC.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly MeetingServiceClient meetingServiceClient = new MeetingServiceClient();

        // GET: Project
        public ActionResult Index(int? page)
        {
            try
            {
	            var viewModel = new IndexViewModel<Meeting>
	            {
		            Pager = meetingServiceClient.ReadMeetingPage(page, null)
	            };
                return View(viewModel);
            }
            catch (FaultException e)
            {
                meetingServiceClient.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
            catch (Exception e)
            {
                meetingServiceClient.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
        }

        public ActionResult Details(Meeting meeting)
        {
            try
            {
                var data = meetingServiceClient.ReadMeeting(meeting);
                meetingServiceClient.Close();
                return View(data);
            }
            catch (FaultException e)
            {
                meetingServiceClient.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
            catch (Exception e)
            {
                meetingServiceClient.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
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
                meeting.CreatedBy.Id = User.Identity.GetUserId();
                meetingServiceClient.CreateMeeting(meeting);
                return RedirectToAction("MyMeetings");
            }
        catch (FaultException e)
            {
                meetingServiceClient.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
            catch (Exception e)
            {
                meetingServiceClient.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
        }

        // GET: Meeting/Edit/5
        public ActionResult Edit(Meeting meeting)
        {
            try
            {
                var data = meetingServiceClient.ReadMeeting(meeting);
                meetingServiceClient.Close();
                return View(data);
            }
            catch (FaultException e)
            {
                meetingServiceClient.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
            catch (Exception e)
            {
                meetingServiceClient.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
        }

        // POST: Meeting/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Meeting meeting)
        {
            try
            {
                meetingServiceClient.UpdateMeeting(meeting);
                return RedirectToAction("MyMeetings"); 
            }
            catch (FaultException e)
            {
                meetingServiceClient.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
            catch (Exception e)
            {
                meetingServiceClient.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
        }

        // GET: Meeting/Delete/5
        public ActionResult Delete(Meeting meeting)
        {
            try
            {
                var data = meetingServiceClient.ReadMeeting(meeting);
                meetingServiceClient.Close();
                return View(data);
            }
            catch (FaultException e)
            {
                meetingServiceClient.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
            catch (Exception e)
            {
                meetingServiceClient.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
        }

        // POST: Meeting/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Meeting meeting)
        {
            try
            {
                meetingServiceClient.DeleteMeeting(meeting);
                return RedirectToAction("MyMeetings");
            }
            catch (FaultException e)
            {
                meetingServiceClient.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
            catch (Exception e)
            {
                meetingServiceClient.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
        }

        public ActionResult MyMeetings(int? page)
        {
            try
            {
	            var viewModel = new IndexViewModel<Meeting>
	            {
					Pager = meetingServiceClient.ReadMeetingPageForUser(HttpContext.User.Identity.GetUserId(),page,null)
	            };
                return View(viewModel);
            }
            catch (FaultException e)
            {
                meetingServiceClient.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
            catch (Exception e)
            {
                meetingServiceClient.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
        }
    }
}
