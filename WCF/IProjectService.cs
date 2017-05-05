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
        // Implementing CRUD as a starting point.
        [OperationContract]
        Project CreateProject(Project t);
        [OperationContract]
        Project ReadProject(Project t);
        [OperationContract]
        Project UpdateProject(Project t);
        [OperationContract]
        Project DeleteProject(Project t);
        // Implementing ReadAll
        [OperationContract]
        List<Project> ReadAllProject();
    }
}
