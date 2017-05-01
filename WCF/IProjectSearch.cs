using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Model;

namespace WCF
{
    [ServiceContract]
    public interface IProjectSearch
    {
        [OperationContract]
        IList<Project> SearchByTag(string search_tag);

        [OperationContract]
        IList<Project> SearchByProjectUser(User user);

        [OperationContract]
        IList<Project> SearchByProjectAddress(string address);
    }
}
