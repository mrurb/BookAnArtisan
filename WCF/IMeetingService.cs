using System;
using System.Collections.Generic;
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
        Meeting CreateMeeting(Meeting t);
        [OperationContract]
        Meeting ReadMeeting(Meeting t);
        [OperationContract]
        Meeting UpdateMeeting(Meeting t);
        [OperationContract]
        Meeting DeleteMeeting(Meeting t);
        // Implementing ReadAll
        [OperationContract]
        List<Meeting> ReadAllMeeting();
        [OperationContract]
        Meeting AddUserToMeeting(Meeting m, User u);

        [OperationContract]
        List<Meeting> ReadAllForUser(User user);
    }
}
