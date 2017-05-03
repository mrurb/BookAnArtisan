using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookAnArtisanMVC.MaterialServiceReference;

namespace BookAnArtisanMVC.Controllers
{
    public class MaterialController : Controller
    {
        MaterialServiceClient ms = new MaterialServiceClient();
        // GET: Material
        public ActionResult Index()
        {
            var data = ms.ReadAll();
            return View(data);
        }

        // GET: Material/Details/5
        public ActionResult Details(Material mat)
        {
            return View();
        }

        // GET: Material/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Material/Create
        [HttpPost]
        public ActionResult Create(Material mat)
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
        public ActionResult Edit(Material mat)
        {
            return View();
        }

        // POST: Material/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Material mat)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Material/Delete/5
        public ActionResult Delete(Material mat)
        {
            return View();
        }

        // POST: Material/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Material mat)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
