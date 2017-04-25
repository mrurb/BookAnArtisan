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
        public Status Create(Status status)
        {
            StatusController statusController = new StatusController();
            return statusController.Create(status);
        }

        public Status Read(Status status)
        {
            StatusController statusController = new StatusController();
            return statusController.Read(status);
        }
        public Status Update(Status status)
        {
            StatusController statusController = new StatusController();
            return statusController.Update(status);
        }
        public Status Delete(Status status)
        {
            StatusController statusController = new StatusController();
            return statusController.Delete(status);
        }

        public List<Status> ReadAll()
        {
            StatusController statusController = new StatusController();
            return statusController.ReadAll();
        }
    }
}
