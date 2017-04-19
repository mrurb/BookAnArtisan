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
        [OperationContract]
        User CreateUser();

        [OperationContract]
        User GetUser(string id);

        [OperationContract]
        User UpdateUser(User user);

        // TODO
        [OperationContract]
        List<User> GetUsers();

        [OperationContract]
        Boolean DeleteUser(string id);

    }
}
