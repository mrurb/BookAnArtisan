using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace WCF
{
    public class UserService : IUserService
    {
        public User Create(User user)
        {
            return user;
        }

        public User Read(int id)
        {
            return new User { ID = "ikdaokodk209k2", Email = "bollocks@bs.dhw" };
        }

        public User Update(User user)
        {
            return new User { Email = "adadaw" };
        }

        public User Delete(User t)
        {
            throw new NotImplementedException();
        }

        public List<User> ReadAll()
        {
            List<User> List = new List<User>();
            List.Add(new User { ID = "akdpawkdaokdoakdw2321", Email = "fucking@whatever.ev" });
            List.Add(new User { ID = "kaoowdk2ke2ojea2", Email = "kwdakdwka" });
            return List;
        }     
    }
}
