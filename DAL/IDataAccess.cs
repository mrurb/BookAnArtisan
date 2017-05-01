using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    // The purpose of this interface, is to keep a consistence between all types of data access classes.
    public interface IDataAccess<T>
    {
        // Implementing CRUD as a starting point.
        T Create(T t);
        T Read(T t);
        T Update(T t);
        T Delete(T t);
        List<T> ReadAll();
    }
}
