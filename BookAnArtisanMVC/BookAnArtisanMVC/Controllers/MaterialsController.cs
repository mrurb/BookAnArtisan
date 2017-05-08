using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookAnArtisanMVC.ServiceReference;

namespace BookAnArtisanMVC.Controllers
{
    public class MaterialsController : Controller
    {
        MaterialServiceClient ms = new MaterialServiceClient();
        // GET: Material
        public ActionResult Index()
        {
            var data = ms.ReadAllMaterial();
            return View(data);
        }

        // GET: Material/Details/5
        public ActionResult Details(Material mat)
        {
            var data = ms.ReadMaterial(mat);
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
                ms.CreateMaterial(mat);
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
            var data = ms.ReadMaterial(mat);
            return View(data);
        }

        // POST: Material/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Material mat)
        {
            try
            {
                ms.UpdateMaterial(mat);
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
            var data = ms.ReadMaterial(mat);
            return View(data);
        }

        // POST: Material/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Material mat)
        {
            try
            {
                ms.DeleteMaterial(mat);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult MyMaterials()
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public JsonResult SearchByName(string name)
        {
            return Json(ms.Search(name), JsonRequestBehavior.AllowGet);
        }
    }
}
