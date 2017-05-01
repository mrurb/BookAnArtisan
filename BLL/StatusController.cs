using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class StatusController : IController<Status>
    {
        StatusDB db = new StatusDB();
        public Status Create(Status status)
        {
            return db.Create(status);
        }
        public Status Read(Status status)
        {
            return db.Read(status);
        }
        public Status Update(Status status)
        {
            return db.Update(status);
        }
        public Status Delete(Status status)
        {
            return db.Delete(status);
        }

        public List<Status> ReadAll()
        {
            return db.ReadAll();
        }

        
    }
}
