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
        public Meeting CreateMeeting(Meeting t)
        {
            return mc.Create(t);
        }

        public Meeting DeleteMeeting(Meeting t)
        {
            return mc.Delete(t);
        }

        public Meeting ReadMeeting(Meeting t)
        {
            return mc.Read(t);
        }

        public List<Meeting> ReadAllMeeting()
        {
            return mc.ReadAll();
        }

        public Meeting UpdateMeeting(Meeting t)
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
