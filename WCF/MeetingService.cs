using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;

namespace WCF
{
    public class MeetingService : IMeetingService
    {
        MeetingController mc = new MeetingController();
        public Meeting Create(Meeting t)
        {
            return mc.Create(t);
        }

        public bool Delete(Meeting t)
        {
            return mc.Delete(t);
        }

        public Meeting Read(Meeting t)
        {
            return mc.Read(t);
        }

        public List<Meeting> ReadAll()
        {
            return mc.ReadAll();
        }

        public Meeting Update(Meeting t)
        {
            return mc.Update(t);
        }

        public Meeting AddUserToMeeting(Meeting m, User u)
        {
            return mc.AddUserToMeeting(m, u);
        }

        public List<Meeting> ReadAllForUser(User user)
        {
            return mc.ReadAllForUser(user);
        }
    }
}
