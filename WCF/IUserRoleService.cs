using Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace WCF
{
	[ServiceContract]
	public interface IUserRoleService
	{
		[OperationContract]
		Role CreateRole(Role t);
		[OperationContract]
		Role ReadRole(Role t);
		[OperationContract]
		Role UpdateRole(Role t);
		[OperationContract]
		Role DeleteRole(Role t);
		[OperationContract]
		List<Role> ReadAllRole();
	}
}
