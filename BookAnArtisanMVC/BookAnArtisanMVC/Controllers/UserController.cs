using System.Web.Mvc;
using BookAnArtisanMVC.ServiceReference;

namespace BookAnArtisanMVC.Controllers
{

	public class UsersController : Controller
	{
		private readonly UserServiceClient userServiceClient = new UserServiceClient();
		public JsonResult SearchByName(string name)
		{
			return Json(userServiceClient.SearchByName(name), JsonRequestBehavior.AllowGet);
		}

		[ValidateAntiForgeryToken]
		public JsonResult SearchByNameVal(string name)
		{
			return Json(userServiceClient.SearchByName(name), JsonRequestBehavior.AllowGet);
		}
	}
}
