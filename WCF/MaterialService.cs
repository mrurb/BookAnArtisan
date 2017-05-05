using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;

namespace WCF
{
    public class MaterialService : IMaterialService
    {
        MaterialController mc = new MaterialController();
        public Material CreateMaterial(Material t)
        {
            return mc.Create(t);
        }

        public Material DeleteMaterial(Material t)
        {
            return mc.Delete(t);
        }

        public Material ReadMaterial(Material t)
        {
            return mc.Read(t);
        }

        public List<Material> ReadAllMaterial()
        {
            return mc.ReadAll();
        }

        public IList<Material> Search(string name)
        {
            return mc.Search(name);
        }

        public Material UpdateMaterial(Material t)
        {
            return mc.Update(t);
        }
    }
}
