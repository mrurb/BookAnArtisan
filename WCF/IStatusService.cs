using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF
{
    [ServiceContract]
    public interface IStatusService
    {
        // Implementing CRUD as a starting point.
        [OperationContract]
        Status CreateStatus(Status t);
        [OperationContract]
        Status ReadStatus(Status t);
        [OperationContract]
        Status UpdateStatus(Status t);
        [OperationContract]
        Status DeleteStatus(Status t);
        // Implementing ReadAll
        [OperationContract]
        List<Status> ReadAllStatus();
    }
}
