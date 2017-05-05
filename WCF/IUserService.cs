using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Model;

namespace WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserService" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {
        // Implementing CRUD as a starting point.
        [OperationContract]
        User CreateUser(User t);
        [OperationContract]
        User ReadUser(User t);
        [OperationContract]
        User UpdateUser(User t);
        [OperationContract]
        User DeleteUser(User t);
        // Implementing ReadAll
        [OperationContract]
        List<User> ReadAllUser();
        [OperationContract]
        IList<User> SearchByName(string name);
    }
}
