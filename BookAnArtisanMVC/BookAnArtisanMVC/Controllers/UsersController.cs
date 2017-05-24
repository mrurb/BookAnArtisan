using System.Web.Mvc;
using BookAnArtisanMVC.ServiceReference;

namespace BookAnArtisanMVC.Controllers
{

	public class UsersController : Controller
	{
		private readonly UserServiceClient userServiceClient = new UserServiceClient();

		public ActionResult Index()
		{
			return View(userServiceClient.ReadAllUser());
		}

		public ActionResult Edit(User user)
		{
			return View(userServiceClient.ReadUser(user));
		}

		[HttpPost, ActionName("Edit")]
		public ActionResult EditConfirmed(User user)
		{
			return View(userServiceClient.UpdateUser(user));
		}

		public ActionResult Delete(User user)
		{
			return View(userServiceClient.ReadUser(user));
		}


		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(User user)
		{
			userServiceClient.DeleteUser(user);
			return RedirectToAction("Index");
		}


		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(User user)
		{
			if (ModelState.IsValid)
			{
				userServiceClient.CreateUser(user);

				return RedirectToAction("Index");
			}
			else
			{
				return View(user);
			}


		}

		public ActionResult Details(User user)
		{
			return View(userServiceClient.ReadUser(user));
		}

		[HttpPost]
		public JsonResult SearchByName(string name)
		{
			return Json(userServiceClient.SearchByName(name), JsonRequestBehavior.AllowGet);
		}
	}
}
