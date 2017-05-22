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
    interface IMaterialService
    {
        // Implementing CRUD as a starting point.
        [OperationContract]
        [FaultContract(typeof(ApplicationException))]
		Material CreateMaterial(Material t);
        [OperationContract]
        [FaultContract(typeof(ApplicationException))]
		Material ReadMaterial(Material t);
        [OperationContract]
        [FaultContract(typeof(ApplicationException))]
		Material UpdateMaterial(Material t);
        [OperationContract]
        [FaultContract(typeof(ApplicationException))]
		Material DeleteMaterial(Material t);
        // Implementing ReadAll
        [OperationContract]
        [FaultContract(typeof(ApplicationException))]
		List<Material> ReadAllMaterial();
        [OperationContract]
        IList<Material> Search(string name);
        [OperationContract]
        [FaultContract(typeof(ApplicationException))]
		List<Material> ReadAllMaterialsForUser(User user);
    }
}
