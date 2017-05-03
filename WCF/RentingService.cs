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
        public Rented Create(Rented t)
        {
            return rc.Create(t);
        }

        public Rented Delete(Rented t)
        {
            return rc.Delete(t);
        }

        public Rented Read(Rented t)
        {
            return rc.Read(t);
        }

        public List<Rented> ReadAll()
        {
            return rc.ReadAll();
        }

        public Rented Update(Rented t)
        {
            return rc.Update(t);
        }
    }
}
