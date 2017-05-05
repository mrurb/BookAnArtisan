using System;
using System.Collections.Generic;
using System.Linq;
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
            return rc.Create(t);
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
