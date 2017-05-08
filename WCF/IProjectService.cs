using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.ServiceModel;

namespace WCF
{
    [ServiceContract]
    public interface IProjectService
    {
        [OperationContract]
        Project CreateProject(Project t);
        [OperationContract]
        Project ReadProject(Project t);
        [OperationContract]
        Project UpdateProject(Project t);
        [OperationContract]
        Project DeleteProject(Project t);
        [OperationContract]
        List<Project> ReadAllProject();
        [OperationContract]
        List<Project> ReadAllProjectsForUser(User user);
    }
}
