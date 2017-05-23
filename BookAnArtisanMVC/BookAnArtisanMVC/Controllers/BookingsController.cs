﻿using System;
using System.ServiceModel;
using System.Web.Mvc;
using BookAnArtisanMVC.Models;
using BookAnArtisanMVC.ServiceReference;

namespace BookAnArtisanMVC.Controllers
{
	public class BookingsController : Controller
	{
		private readonly BookingServiceClient bookingServiceClient = new BookingServiceClient();

		// GET: Booking
		[HttpGet]
		public ActionResult Index(int? page)
		{
			try
			{
				var viewModel = new IndexViewModel
				{
					Pager = bookingServiceClient.ReadPageBooking(page, 10)
				};
				return View(viewModel);
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
				var data = bookingServiceClient.ReadBooking(mat);
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

		public ActionResult CreateById(int id, string name)
		{
			var booking = new Booking()
			{
				Item = new Material()
				{
					Name = name,
					Id = id
				}
			};
			return View("Create", booking);
		}

		// POST: Booking/Create
		[HttpPost]
		public ActionResult Create(Booking mat)
		{
			try
			{
				bookingServiceClient.CreateBooking(mat);
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
				var data = bookingServiceClient.ReadBooking(mat);
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
				bookingServiceClient.UpdateBooking(mat);
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
				var data = bookingServiceClient.ReadBooking(mat);
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
				bookingServiceClient.DeleteBooking(mat);
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
				return View(bookingServiceClient.ReadAllBooking());
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
