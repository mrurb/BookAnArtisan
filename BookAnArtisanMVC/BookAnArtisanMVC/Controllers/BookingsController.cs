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
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
			catch (Exception e)
			{
				ViewBag.ErrorMessage = e.Message;
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
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
			catch (Exception e)
			{
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
		}

		// GET: Booking/Create
		public ActionResult Create()
		{
			return View();
		}
		/*
		public ActionResult Create(int id)
		{
			//ViewBag.MaterialId
			return View();
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
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
			catch (Exception e)
			{
				ViewBag.ErrorMessage = e.Message;
				return View(mat);
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
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
			catch (Exception e)
			{
				ViewBag.ErrorMessage = e.Message;
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
				ViewBag.ErrorMessage = e.Message;
				return View(mat);
			}
			catch (Exception e)
			{
				ViewBag.ErrorMessage = e.Message;
				return View(mat);
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
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
			catch (Exception e)
			{
				ViewBag.ErrorMessage = e.Message;
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
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
			catch (Exception e)
			{
				ViewBag.ErrorMessage = e.Message;
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
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
			catch (Exception e)
			{
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
		}
	}
}
