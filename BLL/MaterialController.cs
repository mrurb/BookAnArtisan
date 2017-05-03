using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
namespace BLL
{
    public class MaterialController : IController<Material>
    {
        MaterialDB mdb = new MaterialDB();
        public Material Create(Material t)
        {
            return mdb.Create(t);
        }

        public Material Delete(Material t)
        {
            throw new NotImplementedException();
        }

        public Material Read(Material t)
        {
            throw new NotImplementedException();
        }

        public List<Material> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Material Update(Material t)
        {
            throw new NotImplementedException();
        }
    }
}
