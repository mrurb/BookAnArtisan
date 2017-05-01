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
        RoleDB db = new RoleDB();
        public Role Create(Role role)
        {
            return db.Create(role);
        }

        public Role Read(Role role)
        {
            return db.Read(role);
        }
        public Role Update(Role role)
        {
            return db.Update(role);
        }
        public Role Delete(Role role)
        {
            return db.Delete(role);
        }

        public List<Role> ReadAll()
        {
            return db.ReadAll();
        }
    }
}
