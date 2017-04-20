using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF
{
    // The purpose of this interface, is to keep a consistence between all types of data access classes.
    [ServiceContract]
    public interface IService<T>
    {
        // Implementing CRUD as a starting point.
        [OperationContract]
        T Create(T t);
        [OperationContract]
        T Read(T t);
        [OperationContract]
        T Update(T t);
        [OperationContract]
        T Delete(T t);
        // Implementing ReadAll
        [OperationContract]
        List<T> ReadAll();
    }
}
