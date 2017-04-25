using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RoleController : IController<Role>
    {
        public Role Create(Role role)
        {
            RoleDB db = new RoleDB();
            return db.Create(role);
        }

        public Role Read(Role role)
        {
            RoleDB db = new RoleDB();
            return db.Read(role);
        }
        public Role Update(Role role)
        {
            RoleDB db = new RoleDB();
            return db.Update(role);
        }
        public Role Delete(Role role)
        {
            RoleDB db = new RoleDB();
            return db.Delete(role);
        }

        public List<Role> ReadAll()
        {
            RoleDB db = new RoleDB();
            return db.ReadAll();
        }
    }
}
