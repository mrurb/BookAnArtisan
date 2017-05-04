using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookAnArtisanMVC.MaterialServiceReference;

namespace BookAnArtisanMVC.Controllers
{
    public class MaterialsController : Controller
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
            var data = ms.Read(mat);
            return View(data);
        }

        // POST: Material/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Material mat)
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
        public ActionResult Delete(Material mat)
        {
            var data = ms.Read(mat);
            return View(data);
        }

        // POST: Material/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Material mat)
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
