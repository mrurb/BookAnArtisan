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
        public User Create(User user)
        {
            UserController userController = new UserController();
            return userController.Create(user);
        }

        public User Read(User user)
        {
            UserController userController = new UserController();
            return userController.Read(user);
        }

        public User Update(User user)
        {
            UserController userController = new UserController();
            return userController.Update(user);
        }

        public User Delete(User user)
        {
            UserController userController = new UserController();
            return userController.Delete(user);
        }

        public List<User> ReadAll()
        {
            UserController userController = new UserController();
            return userController.ReadAll();
        }     
    }
}
