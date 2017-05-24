using System.Collections.Generic;
using System.ServiceModel;
using Model;

namespace WCF
{
	[ServiceContract]
	public interface IStatusService
	{
		[OperationContract]
		Status CreateStatus(Status t);

		[OperationContract]
		Status ReadStatus(Status t);

		[OperationContract]
		Status UpdateStatus(Status t);

		[OperationContract]
		Status DeleteStatus(Status t);

		[OperationContract]
		List<Status> ReadAllStatus();
	}
}