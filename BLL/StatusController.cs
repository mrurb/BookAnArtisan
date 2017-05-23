using DAL;
using Model;
using System.Collections.Generic;

namespace BLL
{
	public class StatusController : IController<Status>
	{
		private readonly StatusDB statusDb = new StatusDB();
		public Status Create(Status status)
		{
			return statusDb.Create(status);
		}
		public Status Read(Status status)
		{
			return statusDb.Read(status);
		}
		public Status Update(Status status)
		{
			return statusDb.Update(status);
		}
		public Status Delete(Status status)
		{
			return statusDb.Delete(status);
		}

		public List<Status> ReadAll()
		{
			return statusDb.ReadAll();
		}


	}
}
