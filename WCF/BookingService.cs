﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using Model;
using BLL;

namespace WCF
{
	public class BookingService : IBookingService
	{
		private readonly BookingController bookingController = new BookingController();
		public Booking CreateBooking(Booking t)
		{
			try
			{
				return bookingController.Create(t);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch
			{
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Unknown Error"));
			}
		}

		public Booking DeleteBooking(Booking t)
		{
			try
			{
				return bookingController.Delete(t);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch
			{
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Unknown Error"));
			}
		}

		public Booking ReadBooking(Booking t)
		{
			try
			{
				return bookingController.Read(t);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch
			{
				
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Unknown Error"));
			}
		}

		public List<Booking> ReadAllBooking()
		{
			try
			{
				return bookingController.ReadAll();
			}
			catch (ApplicationException ex)
			{
				
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch
			{
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Unknown Error"));
			}
		}

		public Page<Booking> ReadPageForUserBooking(string userId, int? page, int? pageSize)
		{
			return bookingController.ReadPage(userId, page, pageSize);
		}

		public Page<Booking> ReadPageBooking(int? page, int? pageSize)
		{
			return bookingController.ReadPage(page, pageSize);
		} 

		public Booking UpdateBooking(Booking t)
		{
			try
			{
				return bookingController.Update(t);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch
			{
				
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}
	}
}
