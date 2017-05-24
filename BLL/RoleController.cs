using DAL;
using Model;
using System.Collections.Generic;

namespace BLL
{
	public class RoleController : IController<Role>
	{
		private readonly UserRoleDb userRoleDb = new UserRoleDb();
		public Role Create(Role role)
		{
			return userRoleDb.Create(role);
		}

		public Role Read(Role role)
		{
			return userRoleDb.Read(role);
		}

		public Role Update(Role role)
		{
			return userRoleDb.Update(role);
		}

		public Role Delete(Role role)
		{
			return userRoleDb.Delete(role);
		}

		public List<Role> ReadAll()
		{
			return userRoleDb.ReadAll();
		}
	}
}
