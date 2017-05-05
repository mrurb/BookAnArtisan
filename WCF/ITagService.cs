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
    public interface ITagService
    {
        // Implementing CRUD as a starting point.
        [OperationContract]
        Tag CreateTag(Tag t);
        [OperationContract]
        Tag ReadTag(Tag t);
        [OperationContract]
        Tag UpdateTag(Tag t);
        [OperationContract]
        Tag DeleteTag(Tag t);
        // Implementing ReadAll
        [OperationContract]
        List<Tag> ReadAllTag();
    }
}
