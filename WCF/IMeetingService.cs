using System;
using System.Collections.Generic;
using System.ServiceModel;
using Model;

namespace WCF
{
	[ServiceContract]
	internal interface IMeetingService
	{
		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		Meeting CreateMeeting(Meeting t);

		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		Meeting ReadMeeting(Meeting t);

		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		Meeting UpdateMeeting(Meeting t);

		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		Meeting DeleteMeeting(Meeting t);

		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		List<Meeting> ReadAllMeeting();

		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		Meeting AddUserToMeeting(Meeting m, User u);

		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		List<Meeting> ReadAllForUser(User user);

		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		Page<Meeting> ReadMeetingPage(int? page, int? pageSize);
		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		Page<Meeting> ReadMeetingPageForUser(string userId, int? page, int? pageSize);
	}
}