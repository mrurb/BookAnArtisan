using System.Collections.Generic;
using BLL;
using Model;

namespace WCF
{
	public class StatusService : IStatusService
	{
		private readonly StatusController statusController = new StatusController();

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