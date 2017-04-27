using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookAnArtisanMVC.UserServiceReference;

namespace BookAnArtisanMVC.Controllers
{

    public class UsersController : Controller
    {
        UserServiceClient uCl = new UserServiceClient();

        public ActionResult Index()
        {
            return View(uCl.ReadAll());
        }

        public ActionResult Edit(User user)
        {
            return View(uCl.Read(user));
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(User user)
        {
            return View(uCl.Update(user));
        }

        public ActionResult Delete(User user)
        {
            return View(uCl.Read(user));
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(User user)
        {
            uCl.Delete(user);
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
                uCl.Create(user);

                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }


        }

        public ActionResult Details(User user)
        {
            return View(uCl.Read(user));
        }

        [HttpPost]
        public JsonResult SearchByName(string name)
        {
            return Json(uCl.SearchByName(name), JsonRequestBehavior.AllowGet);
        }
    }
}
