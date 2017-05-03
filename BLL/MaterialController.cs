﻿using System;
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
            mdb.Delete(t);
            return mdb.Read(t);
        }

        public Material Read(Material t)
        {
            return mdb.Read(t);
        }

        public List<Material> ReadAll()
        {
            return mdb.ReadAll();
        }

        public Material Update(Material t)
        {
            mdb.Update(t);
            return mdb.Read(t);
        }


        public IList<Material> SearchByMaterialName(string name)
        {
            return mdb.SearchByMaterialName(name);
        }
    }
}
