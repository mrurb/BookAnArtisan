using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookAnArtisanMVC.Controllers
{

    public class UsersController : Controller
    {
        UserServiceReference.UserServiceClient uCl = new UserServiceReference.UserServiceClient();

        public ActionResult Index()
        {
            return View(uCl.GetUsers());
        }

        public ActionResult Edit(string id)
        {
            return View(uCl.GetUser(id));
        }

        public ActionResult Delete(string id)
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


        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserServiceReference.User user)
        {
            if(ModelState.IsValid)
            {
                uCl.CreateUser(user);

                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }

            
        }


        public ActionResult Details(string id)
        {
            return View(uCl.GetUser(id));
        }
    }
}
