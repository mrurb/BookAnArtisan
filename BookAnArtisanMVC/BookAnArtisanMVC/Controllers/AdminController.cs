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

        public ActionResult EditUser(int id)
        {
            
            return View(uCl.GetUser(id));
        }

    }
}
