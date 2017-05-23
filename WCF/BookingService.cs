using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;

namespace WCF
{
	public class BookingService : IBookingService
	{
		BookingController rc = new BookingController();
		public Booking CreateBooking(Booking t)
		{
			try
			{
				return rc.Create(t);
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
				return rc.Delete(t);
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
				return rc.Read(t);
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
				return rc.ReadAll();
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
			return rc.ReadPage(page, pageSize);
		}

		public Booking UpdateBooking(Booking t)
		{
			try
			{
				return rc.Update(t);
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
