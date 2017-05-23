using System.Collections.Generic;
using Model;
using DAL;

namespace BLL
{
	public class UserController : IController<User>
	{
		private readonly UserDB userDb = new UserDB();
		public User Create(User user)
		{
			return userDb.Create(user);
		}

		public User Read(User user)
		{
			return userDb.Read(user);
		}

		public User Update(User user)
		{
			return userDb.Update(user);
		}
		public User Delete(User user)
		{
			return userDb.Delete(user);
		}

		public List<User> ReadAll()
		{
			return userDb.ReadAll();
		}

		public IList<User> SearchByName(string name)
		{
			return userDb.SearchByName(name);
		}
	}
}
