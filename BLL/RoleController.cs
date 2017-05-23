using DAL;
using Model;
using System.Collections.Generic;

namespace BLL
{
	public class RoleController : IController<Role>
	{
		private readonly RoleDB roleDb = new RoleDB();
		public Role Create(Role role)
		{
			return roleDb.Create(role);
		}

		public Role Read(Role role)
		{
			return roleDb.Read(role);
		}

		public Role Update(Role role)
		{
			return roleDb.Update(role);
		}

		public Role Delete(Role role)
		{
			return roleDb.Delete(role);
		}

		public List<Role> ReadAll()
		{
			return roleDb.ReadAll();
		}
	}
}
