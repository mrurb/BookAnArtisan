using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Model;

namespace WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        public User CreateUser()
        {
            return new User { Email = "kdawkdwk@akdkwa.od"};
        }

        public Boolean DeleteUser(User user)
        {
            return true;
        }

        public User GetUser(int ID)
        {
            return new User { Email = "bollocks@bs.dhw"};
        }

        public List<User> GetUsers()
        {
            List<User> List = new List<User>();
            List.Add(new User { Email = "fucking@whatever.ev"});
            List.Add(new User { Email = "kwdakdwka" });
            return List;
        }

        public User UpdateUser(User user)
        {
            return new User { Email = "adadaw"};
        }
    }
}
