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
        public Status CreateStatus(Status status)
        {
            return statusController.Create(status);
        }

        public Status ReadStatus(Status status)
        {
            return statusController.Read(status);
        }
        public Status UpdateStatus(Status status)
        {
            return statusController.Update(status);
        }
        public Status DeleteStatus(Status status)
        {
            return statusController.Delete(status);
        }

        public List<Status> ReadAllStatus()
        {
            return statusController.ReadAll();
        }
    }
}
