using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    // The purpose of this interface, is to keep a consistence between all types of controller classes.
    public interface IController<T>
    {
        // Implementing CRUD as a starting point.
        T Create(T t);
        T Read(T t);
        T Update(T t);
        bool Delete(T t);
        List<T> ReadAll();
    }
}
