using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class UserController : IController<User>
    {
        UserDB db = new UserDB();
        public User Create(User user)
        {
            return db.Create(user);
        }

        public User Read(User user)
        {
            return db.Read(user);
        }

        public User Update(User user)
        {
            return db.Update(user);
        }
        public bool Delete(User user)
        {
            return db.Delete(user);
        }

        public List<User> ReadAll()
        {
            return db.ReadAll();
        }

        public IList<User> SearchByName(string name)
        {
            return db.SearchByName(name);
        }
    }
}
