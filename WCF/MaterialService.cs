﻿using System;
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
        public Material Create(Material t)
        {
            return mc.Create(t);
        }

        public Material Delete(Material t)
        {
            return mc.Delete(t);
        }

        public Material Read(Material t)
        {
            return mc.Read(t);
        }

        public List<Material> ReadAll()
        {
            return mc.ReadAll();
        }

        public Material Update(Material t)
        {
            return mc.Update(t);
        }
    }
}
