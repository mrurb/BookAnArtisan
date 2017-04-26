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
        public User Create(User user)
        {
            return userController.Create(user);
        }

        public User Read(User user)
        {
            return userController.Read(user);
        }

        public User Update(User user)
        {
            return userController.Update(user);
        }

        public bool Delete(User user)
        {
            return userController.Delete(user);
        }

        public List<User> ReadAll()
        {
            return userController.ReadAll();
        }     
    }
}
