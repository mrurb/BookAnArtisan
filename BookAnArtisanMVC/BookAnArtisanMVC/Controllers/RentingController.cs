using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookAnArtisanMVC.RentingServiceReference;

namespace BookAnArtisanMVC.Controllers
{
    public class RentingController : Controller
    {
        RentingServiceClient ms = new RentingServiceClient();
        // GET: Material
        public ActionResult Index()
        {
            var data = ms.ReadAll();
            return View(data);
        }

        // GET: Material/Details/5
        public ActionResult Details(Rented mat)
        {
            var data = ms.Read(mat);
            return View(data);
        }

        // GET: Material/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Material/Create
        [HttpPost]
        public ActionResult Create(Rented mat)
        {
            try
            {
                ms.Create(mat);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Material/Edit/5
        public ActionResult Edit(Rented mat)
        {
            var data = ms.Read(mat);
            return View(data);
        }

        // POST: Material/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Rented mat)
        {
            try
            {
                ms.Update(mat);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Material/Delete/5
        public ActionResult Delete(Rented mat)
        {
            var data = ms.Read(mat);
            return View(data);
        }

        // POST: Material/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Rented mat)
        {
            try
            {
                ms.Delete(mat);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
