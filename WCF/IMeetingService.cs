﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.ServiceModel;

namespace WCF
{
    [ServiceContract]
    interface IMeetingService : IService<Meeting>
    {
        [OperationContract]
        Meeting AddUserToMeeting(Meeting m, User u);
    }
}
