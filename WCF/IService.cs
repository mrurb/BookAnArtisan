using System.Collections.Generic;
using System.ServiceModel;

namespace WCF
{
	// The purpose of this interface, is to keep a consistence between all types of data access classes.
	[ServiceContract]
	public interface IService<T>
	{
		[OperationContract]
		T Create(T t);

		[OperationContract]
		T Read(T t);

		[OperationContract]
		T Update(T t);

		[OperationContract]
		T Delete(T t);

		[OperationContract]
		List<T> ReadAll();
	}
}