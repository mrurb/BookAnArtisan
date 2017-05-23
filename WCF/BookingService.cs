using System;
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
			catch (Exception ex)
			{
				//log(ex);
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
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
			catch (Exception ex)
			{
				//log(ex);
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
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
			catch (Exception ex)
			{
				//log(ex);
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
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
				//log(ex);
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch (Exception ex)
			{
				//log(ex);
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		public Page<Booking> ReadPageBooking(int? page, int? pageSize)
		{
			var paget = bookingController.ReadPage(page, pageSize);
			return paget;
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
			catch (Exception ex)
			{
				//log(ex);
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}
	}
}
