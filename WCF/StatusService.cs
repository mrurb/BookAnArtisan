using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;

namespace WCF
{
    public class StatusService : IStatusService
    {
        StatusController statusController = new StatusController();
        public Status Create(Status status)
        {
            return statusController.Create(status);
        }

        public Status Read(Status status)
        {
            return statusController.Read(status);
        }
        public Status Update(Status status)
        {
            return statusController.Update(status);
        }
        public bool Delete(Status status)
        {
            return statusController.Delete(status);
        }

        public List<Status> ReadAll()
        {
            return statusController.ReadAll();
        }
    }
}
