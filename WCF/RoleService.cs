using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;

namespace WCF
{
    public class RoleService : IRoleService
    {
        RoleController roleController = new RoleController();
        public Role Create(Role role)
        {
            return roleController.Create(role);
        }

        public Role Read(Role role)
        {
            return roleController.Read(role);
        }
        public Role Update(Role role)
        {
            return roleController.Update(role);
        }
        public Role Delete(Role role)
        {
            return roleController.Delete(role);
        }

        public List<Role> ReadAll()
        {
            return roleController.ReadAll();
        }
    }
}
