using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;

namespace WCF
{
    public class UserService : IUserService
    {
            UserController userController = new UserController();
        public User CreateUser(User user)
        {
            return userController.Create(user);
        }

        public User ReadUser(User user)
        {
            return userController.Read(user);
        }

        public User UpdateUser(User user)
        {
            return userController.Update(user);
        }

        public User DeleteUser(User user)
        {
            return userController.Delete(user);
        }

        public List<User> ReadAllUser()
        {
            return userController.ReadAll();
        }

        public IList<User> SearchByName(string name)
        {
            return userController.SearchByName(name);
        }
    }
}
