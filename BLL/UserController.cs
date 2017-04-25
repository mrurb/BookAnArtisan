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
        public User Create(User user)
        {
            UserDB db = new UserDB();
            return db.Create(user);
        }

        public User Read(User user)
        {
            UserDB db = new UserDB();
            return db.Read(user);
        }

        public User Update(User user)
        {
            UserDB db = new UserDB();
            return db.Update(user);
        }
        public User Delete(User user)
        {
            UserDB db = new UserDB();
            return db.Delete(user);
        }

        public List<User> ReadAll()
        {
            UserDB db = new UserDB();
            return db.ReadAll();
        }
    }
}
