using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class RentingController : IController<Rented>
    {
        RentingDB rdb = new RentingDB();
        public Rented Create(Rented t)
        {
            return rdb.Create(t);
        }

        public Rented Delete(Rented t)
        {
            rdb.Delete(t);
            return rdb.Read(t);
        }

        public Rented Read(Rented t)
        {
            return rdb.Read(t);
        }

        public List<Rented> ReadAll()
        {
            return rdb.ReadAll();
        }

        public Rented Update(Rented t)
        {
            return rdb.Update(t);
        }
    }
}
