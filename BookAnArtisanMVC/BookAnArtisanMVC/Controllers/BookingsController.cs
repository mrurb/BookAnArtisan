using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using BookAnArtisanMVC.ServiceReference;

namespace BookAnArtisanMVC.Controllers
{
	public class BookingsController : Controller
	{
		private RentingServiceClient ms = new RentingServiceClient();
		// GET: Booking
		public ActionResult Index()
		{
			try
			{
				var data = ms.ReadAllBooking();
				return View(data);
			}
			catch (FaultException e)
			{
				return View("Error", new HandleErrorInfo(e, "Bookings", "Index"));
			}
			catch
			{
				return View();
			}
		}

		// GET: Booking/Details/5
		public ActionResult Details(Booking mat)
		{
			try
			{
				var data = ms.ReadBooking(mat);
				return View(data);
			}
			catch (FaultException e)
			{
				return View("Error", new HandleErrorInfo(e, "Bookings", "Details"));
			}
			catch
			{
				return View();
			}
		}

		// GET: Booking/Create
		public ActionResult Create()
		{
			return View();
		}
		/*
		public ActionResult Create(Material material)
		{
			
			return View(new Booking() {Item = material });
		}
		*/
		// POST: Booking/Create
		[HttpPost]
		public ActionResult Create(Booking mat)
		{
			try
			{
				ms.CreateBooking(mat);
				return RedirectToAction("Index");
			}
			catch (FaultException e)
			{
				return View("Error", new HandleErrorInfo(e, "Bookings", "Create"));
			}
			catch
			{
				return View();
			}
		}

		// GET: Booking/Edit/5
		public ActionResult Edit(Booking mat)
		{
			try
			{
				var data = ms.ReadBooking(mat);
				return View(data);
			}
			catch (FaultException e)
			{
				return View("Error", new HandleErrorInfo(e, "Bookings", "Edit"));
			}
			catch
			{
				return View();
			}
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
			catch (FaultException e)
			{
				return View("Error", new HandleErrorInfo(e, "Bookings", "Edit"));
			}
			catch
			{
				return View();
			}
		}

		// GET: Booking/Delete/5
		public ActionResult Delete(Booking mat)
		{
			try
			{
				var data = ms.ReadBooking(mat);
				return View(data);
			}
			catch (FaultException e)
			{
				return View("Error", new HandleErrorInfo(e, "Bookings", "Delete"));
			}
			catch
			{
				return View();
			}
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
			catch (FaultException e)
			{
				return View("Error", new HandleErrorInfo(e, "Bookings", "Delete"));
			}
			catch
			{
				return View();
			}
		}

		public ActionResult MyBooking(User user)
		{
			try
			{
				return View(ms.ReadAllBooking());
			}
			catch (FaultException e)
			{
				return View("Error", new HandleErrorInfo(e, "Bookings", "MyBooking"));
			}
			catch
			{
				return View();
			}
		}
	}
}
