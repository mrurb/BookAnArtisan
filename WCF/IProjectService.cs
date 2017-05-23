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
        [FaultContract(typeof(ApplicationException))]
		Project CreateProject(Project t);
        [OperationContract]
        [FaultContract(typeof(ApplicationException))]
		Project ReadProject(Project t);
        [OperationContract]
        [FaultContract(typeof(ApplicationException))]
		Project UpdateProject(Project t);
        [OperationContract]
        [FaultContract(typeof(ApplicationException))]
		Project DeleteProject(Project t);
        [OperationContract]
        [FaultContract(typeof(ApplicationException))]
		List<Project> ReadAllProject();
        [OperationContract]
        [FaultContract(typeof(ApplicationException))]
		List<Project> ReadAllProjectsForUser(User user);

	    [OperationContract]
	    [FaultContract(typeof(ApplicationException))]
	    Page<Booking> ReadPage(int? page, int? pageSize);
	    [OperationContract]
	    [FaultContract(typeof(ApplicationException))]
	    Page<Booking> ReadPageForUser(int? page, int? pageSize);
	}
}
