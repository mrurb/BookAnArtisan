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
        Material CreateMaterial(Material t);
        [OperationContract]
        Material ReadMaterial(Material t);
        [OperationContract]
        Material UpdateMaterial(Material t);
        [OperationContract]
        Material DeleteMaterial(Material t);
        // Implementing ReadAll
        [OperationContract]
        List<Material> ReadAllMaterial();
        [OperationContract]
        IList<Material> Search(string name);
        [OperationContract]
        List<Material> ReadAllMaterialsForUser(User user);
    }
}
