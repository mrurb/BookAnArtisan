using System.Collections.Generic;
using System.ServiceModel;
using Model;

namespace WCF
{
	[ServiceContract]
	public interface ITagService
	{
		[OperationContract]
		Tag CreateTag(Tag t);

		[OperationContract]
		Tag ReadTag(Tag t);

		[OperationContract]
		Tag UpdateTag(Tag t);

		[OperationContract]
		Tag DeleteTag(Tag t);

		[OperationContract]
		List<Tag> ReadAllTag();
	}
}