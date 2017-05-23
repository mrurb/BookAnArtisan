using System;
using System.Collections.Generic;
using Model;
using DAL;

namespace BLL
{
	public class MeetingController : IController<Meeting>
	{
		private readonly MeetingDB meetingDb = new MeetingDB();
		private readonly UserController userController = new UserController();
		public Meeting Create(Meeting t)
		{
			if (t.StartTime >= t.EndTime)
			{
				throw new ApplicationException("Incorrect Dates.");
			}
			var contact = userController.Read(t.Contact);
			if (contact.UserName != t.Contact.UserName)
			{
				throw new ApplicationException("User: Contact invalid.");
			}
			return meetingDb.Create(t);
		}

		public Meeting Delete(Meeting t)
		{
			meetingDb.Delete(t);
			return meetingDb.ReadDetails(t);
		}

		public Meeting Read(Meeting t)
		{
			return meetingDb.ReadDetails(t);
		}

		public List<Meeting> ReadAll()
		{
			return meetingDb.ReadAll();
		}

		public Meeting Update(Meeting t)
		{
			return meetingDb.Update(t);
		}

		public Meeting AddUserToMeeting(Meeting m, User u)
		{
			return meetingDb.AddUserToMeeting(m, u);
		}

		public List<Meeting> ReadAllForUser(User user)
		{
			if (user == null)
			{
				throw new ApplicationException("No user selected.");
			}
			return meetingDb.ReadAllForUser(user);
		}
	}
}
