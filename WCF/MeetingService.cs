using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
            try
            {
                return mc.Create(t);
            }
            catch (ApplicationException e)
            {
                throw new FaultException<ApplicationException>(e, new FaultReason(e.Message), new FaultCode("Sender"));
            }
            catch (Exception e)
            {
                throw new FaultException<ApplicationException>((ApplicationException)e, new FaultReason(e.Message), new FaultCode("Sender"));

            }
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
