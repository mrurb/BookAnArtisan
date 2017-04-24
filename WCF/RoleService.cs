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
        public Role Create(Role role)
        {
            RoleController roleController = new RoleController();
            return roleController.Create(role);
        }

        public Role Read(Role role)
        {
            RoleController roleController = new RoleController();
            return roleController.Read(role);
        }
        public Role Update(Role role)
        {
            RoleController roleController = new RoleController();
            return roleController.Update(role);
        }
        public Role Delete(Role role)
        {
            RoleController roleController = new RoleController();
            return roleController.Delete(role);
        }

        public List<Role> ReadAll()
        {
            RoleController roleController = new RoleController();
            return roleController.ReadAll();
        }
    }
}
