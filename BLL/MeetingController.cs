using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
using System.ServiceModel;

namespace BLL
{
    public class MeetingController : IController<Meeting>
    {
        MeetingDB mdb = new MeetingDB();
        public Meeting Create(Meeting t)
        {
            return mdb.Create(t);
        }

        public Meeting Delete(Meeting t)
        {
            try
            {
                mdb.Delete(t);
                return mdb.ReadDetails(t);
            }
            catch (FaultException e)
            {
                throw;
            }
        }

        public Meeting Read(Meeting t)
        {
            return mdb.ReadDetails(t);
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
