using System;
using System.Collections.Generic;
using System.ServiceModel;
using Model;
using DAL;

namespace BLL
{
    public class RentingController : IController<Booking>
    {
        RentingDb rdb = new RentingDb();
        public Booking Create(Booking t)
        {
		        return rdb.Create(t);
        }

        public Booking Delete(Booking t)
        {
            rdb.Delete(t);
            return rdb.Read(t);
        }

        public Booking Read(Booking t)
        {
            return rdb.Read(t);
        }

        public List<Booking> ReadAll()
        {
            return rdb.ReadAll();
        }

        public Booking Update(Booking t)
        {
            return rdb.Update(t);
        }
    }
}
