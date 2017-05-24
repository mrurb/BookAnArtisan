using System;
using System.Collections.Generic;
using System.ServiceModel;
using Model;

namespace WCF
{
	[ServiceContract]
	public interface IUserService
	{
		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		User CreateUser(User t);

		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		User ReadUser(User t);

		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		User UpdateUser(User t);

		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		User DeleteUser(User t);

		[OperationContract]
		[FaultContract(typeof(ApplicationException))]
		List<User> ReadAllUser();

		[OperationContract]
		IList<User> SearchByName(string name);
	}
}