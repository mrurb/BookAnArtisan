using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
namespace BLL
{
    public class MeetingController : IController<Meeting>
    {
        MeetingDB mdb = new MeetingDB();
        public Meeting Create(Meeting t)
        {
            return mdb.Create(t);
        }

        public bool Delete(Meeting t)
        {
            return mdb.Delete(t);
        }

        public Meeting Read(Meeting t)
        {
            return mdb.Read(t);
        }

        public List<Meeting> ReadAll()
        {
            return mdb.ReadAll();
        }

        public Meeting Update(Meeting t)
        {
            return mdb.Update(t);
        }

        public Meeting AddUserToMeeting(Meeting m, User u)
        {
            return mdb.AddUserToMeeting(m, u);
        }

        public List<Meeting> ReadAllForUser(User user)
        {
            return mdb.ReadAllForUser(user);
        }
    }
}
