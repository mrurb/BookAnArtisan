using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ICRUD<T>
    {
        T Create(T t);
        T Read(int id);
        T Update(T t);
        T Delete(T t);
    }
}
