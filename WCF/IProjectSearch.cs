using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Model;

namespace WCF
{
    [ServiceContract]
    public interface IProjectSearch
    {
        [OperationContract]
        IList<Project> search_by_tag(string search_tag);

        [OperationContract]
        IList<Project> search_by_project_name(string pname);

        [OperationContract]
        IList<Project> search_by_project_status(bool status);

        [OperationContract]
        IList<Project> search_by_project_user(User user);

        [OperationContract]
        IList<Project> search_by_project_address(string address);
    }
}
