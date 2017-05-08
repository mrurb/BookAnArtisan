using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookAnArtisanMVC.ServiceReference;

namespace BookAnArtisanMVC.Controllers
{
    public class BookingsController : Controller
    {

        RentingServiceClient ms = new RentingServiceClient();
        // GET: Booking
        public ActionResult Index()
        {
            var data = ms.ReadAllBooking();
            return View(data);
        }

        // GET: Booking/Details/5
        public ActionResult Details(Booking mat)
        {
            var data = ms.ReadBooking(mat);
            return View(data);
        }

        // GET: Booking/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        public ActionResult Create(Booking mat)
        {
            try
            {
                ms.CreateBooking(mat);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(Booking mat)
        {
            var data = ms.ReadBooking(mat);
            return View(data);
        }

        // POST: Booking/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Booking mat)
        {
            try
            {
                ms.UpdateBooking(mat);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(Booking mat)
        {
            var data = ms.ReadBooking(mat);
            return View(data);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Booking mat)
        {
            try
            {
                ms.DeleteBooking(mat);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult MyBooking(User user)
        {
            return View(ms.ReadAllBooking());
        }
    }
}
