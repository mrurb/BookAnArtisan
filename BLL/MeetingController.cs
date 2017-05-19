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
		UserController uctr = new UserController();
        public Meeting Create(Meeting t)
        {
	        if (t.StartTime >= t.EndTime)
	        {
		        throw new ApplicationException("Incorrect Dates.");
	        }
	        var contact = uctr.Read(t.Contact);
	        if (contact.UserName != t.Contact.UserName)
	        {
		        throw new ApplicationException("User: Contact invalid.");
	        }
            return mdb.Create(t);
        }

        public Meeting Delete(Meeting t)
        {
            mdb.Delete(t);
            return mdb.ReadDetails(t);
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
	        if (user == null)
	        {
		        throw new ApplicationException("No user selected.");
	        }
            return mdb.ReadAllForUser(user);
        }
    }
}
