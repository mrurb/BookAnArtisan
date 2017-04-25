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
        public Status Create(Status status)
        {
            StatusDB db = new StatusDB();
            return db.Create(status);
        }
        public Status Read(Status status)
        {
            StatusDB db = new StatusDB();
            return db.Read(status);
        }
        public Status Update(Status status)
        {
            StatusDB db = new StatusDB();
            return db.Update(status);
        }
        public Status Delete(Status status)
        {
            StatusDB db = new StatusDB();
            return db.Delete(status);
        }

        public List<Status> ReadAll()
        {
            StatusDB db = new StatusDB();
            return db.ReadAll();
        }

        
    }
}
