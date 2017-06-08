using System;
using System.Collections.Generic;
using Model;
using BLL;
using System.ServiceModel;

namespace WCF
{
	public class UserService : IUserService
	{
		private readonly UserController userController = new UserController();
		public User CreateUser(User user)
		{
			try
			{
				return userController.Create(user);
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

		public User ReadUser(User user)
		{
			try
			{
				return userController.Read(user);
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

		public User UpdateUser(User user)
		{
			try
			{
				return userController.Update(user);
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

		public User DeleteUser(User user)
		{
			try
			{
				return userController.Delete(user);
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

		public List<User> ReadAllUser()
		{
			try
			{
				return userController.ReadAll();
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

		public IList<User> SearchByName(string name)
		{
			return userController.SearchByName(name);
		}
	}
}
