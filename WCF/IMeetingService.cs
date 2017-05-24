using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.ServiceModel;

namespace WCF
{
	[ServiceContract]
	interface IMeetingService
	{
		// Implementing CRUD as a starting point.
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
		// Implementing ReadAll
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
