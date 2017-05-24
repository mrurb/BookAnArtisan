using System.Collections.Generic;
using Model;
using BLL;

namespace WCF
{
	public class UserRoleService : IUserRoleService
	{
		private readonly RoleController roleController = new RoleController();
		public Role CreateRole(Role role)
		{
			return roleController.Create(role);
		}

		public Role ReadRole(Role role)
		{
			return roleController.Read(role);
		}
		public Role UpdateRole(Role role)
		{
			return roleController.Update(role);
		}
		public Role DeleteRole(Role role)
		{
			return roleController.Delete(role);
		}

		public List<Role> ReadAllRole()
		{
			return roleController.ReadAll();
		}
	}
}
