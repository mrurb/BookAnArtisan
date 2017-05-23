using System;
using System.Collections.Generic;
using Model;
using BLL;
using System.ServiceModel;

namespace WCF
{
	public class MaterialService : IMaterialService
	{
		private readonly MaterialController mc = new MaterialController();
		public Material CreateMaterial(Material t)
		{
			try
			{
				return mc.Create(t);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch
			{
				
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
			
		}

		public Material DeleteMaterial(Material t)
		{
			try
			{
				return mc.Delete(t);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch
			{
				
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		public Material ReadMaterial(Material t)
		{
			try
			{
				return mc.Read(t);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch
			{
				
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		public List<Material> ReadAllMaterial()
		{
			try
			{
				return mc.ReadAll();
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch
			{
				
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		public IList<Material> Search(string name)
		{
			return mc.Search(name);
		}

		public Material UpdateMaterial(Material t)
		{
			try
			{
				return mc.Update(t);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch
			{
				
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		public List<Material> ReadAllMaterialsForUser(User user)
		{
			try
			{
				return mc.ReadAllForUser(user);
			}
			catch (ApplicationException ex)
			{
				throw new FaultException<ApplicationException>(ex, new FaultReason(ex.Message), new FaultCode("Sender"));
			}
			catch
			{
				
				var ex2 = new ApplicationException(@"Unknown Error");
				throw new FaultException<ApplicationException>(ex2, new FaultReason(ex2.Message), new FaultCode("Uknown Error"));
			}
		}

		public Page<Material> ReadMaterialPage(int? page, int? pageSize)
		{
			throw new NotImplementedException();
		}

		public Page<Material> ReadMaterialPageForUser(int? page, int? pageSize)
		{
			throw new NotImplementedException();
		}
	}
}
