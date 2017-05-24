using System;
using System.Collections.Generic;
using System.ServiceModel;
using Model;
using BLL;

namespace WCF
{
	public class MeetingService : IMeetingService
	{
		private readonly MeetingController meetingController = new MeetingController();
		public Meeting CreateMeeting(Meeting t)
		{
			try
			{
				return meetingController.Create(t);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch (Exception)
			{
				
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		public Meeting DeleteMeeting(Meeting t)
		{
			try
			{
				return meetingController.Delete(t);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch (Exception)
			{
				
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		public Meeting ReadMeeting(Meeting t)
		{
			try
			{
				return meetingController.Read(t);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch (Exception)
			{
				
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		public List<Meeting> ReadAllMeeting()
		{
			try
			{
				return meetingController.ReadAll();
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch (Exception)
			{
				
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		public Meeting UpdateMeeting(Meeting t)
		{
			try
			{
				return meetingController.Update(t);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch (Exception)
			{
				
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		public Meeting AddUserToMeeting(Meeting m, User u)
		{
			try
			{
				return meetingController.AddUserToMeeting(m,u);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch (Exception)
			{
				
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		public List<Meeting> ReadAllForUser(User user)
		{
			try
			{
				return meetingController.ReadAllForUser(user);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch (Exception)
			{
				
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		public Page<Meeting> ReadMeetingPage(int? page, int? pageSize)
		{
			return meetingController.ReadMeetingPage(page, pageSize);
		}

		public Page<Meeting> ReadMeetingPageForUser(string userId, int? page, int? pageSize)
		{
			return meetingController.ReadMeetingPageForUser(userId, page, pageSize);
		}
	}
}
