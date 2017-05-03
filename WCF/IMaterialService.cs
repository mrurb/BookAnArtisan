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
    interface IMaterialService : IService<Material>
    {
        [OperationContract]
        IList<Material> SearchByName(string name);
    }
}
