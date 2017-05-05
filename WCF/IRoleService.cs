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
    public interface IRoleService
    {
        // Implementing CRUD as a starting point.
        [OperationContract]
        Role CreateRole(Role t);
        [OperationContract]
        Role ReadRole(Role t);
        [OperationContract]
        Role UpdateRole(Role t);
        [OperationContract]
        Role DeleteRole(Role t);
        // Implementing ReadAll
        [OperationContract]
        List<Role> ReadAllRole();
    }
}
