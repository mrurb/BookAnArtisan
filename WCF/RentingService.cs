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
        public Booking Create(Booking t)
        {
            return rc.Create(t);
        }

        public Booking Delete(Booking t)
        {
            return rc.Delete(t);
        }

        public Booking Read(Booking t)
        {
            return rc.Read(t);
        }

        public List<Booking> ReadAll()
        {
            return rc.ReadAll();
        }

        public Booking Update(Booking t)
        {
            return rc.Update(t);
        }
    }
}
