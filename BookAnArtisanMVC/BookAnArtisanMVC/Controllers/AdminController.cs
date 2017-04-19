using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookAnArtisanMVC.Controllers
{

    public class AdminController : Controller
    {
        UserServiceReference.UserServiceClient uCl = new UserServiceReference.UserServiceClient();

        public ActionResult Index()
        {
            return View(uCl.GetUsers());
        }

        public ActionResult EditUser(string id)
        {
            return View(uCl.GetUser(id));
        }

        public ActionResult DeleteUser(string id)
        {
            return View(uCl.GetUser(id));
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            uCl.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
