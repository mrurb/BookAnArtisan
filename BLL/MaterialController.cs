using System;
using System.Collections.Generic;
using Model;
using DAL;
namespace BLL
{
	public class MaterialController : IController<Material>
	{
		private readonly MaterialDB materialDb = new MaterialDB();
		public Material Create(Material t)
		{
			return materialDb.Create(t);
		}

		public Material Delete(Material t)
		{
			materialDb.Delete(t);
			return materialDb.Read(t);
		}

		public Material Read(Material t)
		{
			return materialDb.Read(t);
		}

		public List<Material> ReadAll()
		{
			return materialDb.ReadAll();
		}

		public Material Update(Material t)
		{
			materialDb.Update(t);
			return materialDb.Read(t);
		}


		public IList<Material> Search(string name)
		{
			return materialDb.SearchMaterials(name);
		}

		public List<Material> ReadAllForUser(User user)
		{
			if (user == null)
			{
				throw new ApplicationException("No user selected.");
			}
			return materialDb.ReadAllForUser(user);
		}
	}
}
