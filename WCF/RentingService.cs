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
    public class RentingService : IRentingService
    {
        RentingController rc = new RentingController();
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
		        var ex2 = new ApplicationException("fejl");
		        throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("get fuked"));
	        }
        }

        public Booking DeleteBooking(Booking t)
        {
            return rc.Delete(t);
        }

        public Booking ReadBooking(Booking t)
        {
            return rc.Read(t);
        }

        public List<Booking> ReadAllBooking()
        {
            return rc.ReadAll();
        }

        public Booking UpdateBooking(Booking t)
        {
            return rc.Update(t);
        }
    }
}
