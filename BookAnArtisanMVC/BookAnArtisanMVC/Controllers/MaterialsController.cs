using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using BookAnArtisanMVC.ServiceReference;
using Microsoft.AspNet.Identity;

namespace BookAnArtisanMVC.Controllers
{
    public class MaterialsController : Controller
    {
        MaterialServiceClient ms = new MaterialServiceClient();
        // GET: Material
        public ActionResult Index()
        {
            try
            {
                var data = ms.ReadAllMaterial();
                return View(data);
            }
            catch (FaultException e)
            {
                ms.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
            catch (Exception e)
            {
                ms.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
        }

        // GET: Material/Details/5
        public ActionResult Details(Material mat)
        {
            try
            {
                var data = ms.ReadMaterial(mat);
                return View(data);
            }
            catch (FaultException e)
            {
                ms.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
            catch (Exception e)
            {
                ms.Abort();
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
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
			catch (FaultException e)
            {
	            ms.Abort();
	            ViewBag.ErrorMessage = e.Message;
	            return View(mat);
            }
            catch (Exception e)
            {
	            ms.Abort();
	            ViewBag.ErrorMessage = e.Message;
	            return View(mat);
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
			catch (FaultException e)
            {
	            ms.Abort();
	            ViewBag.ErrorMessage = e.Message;
	            return View(mat);
            }
            catch (Exception e)
            {
	            ms.Abort();
	            ViewBag.ErrorMessage = e.Message;
	            return View(mat);
            }
		}

        // GET: Material/Delete/5
        public ActionResult Delete(Material mat)
        {
	        try
	        {
		        var data = ms.ReadMaterial(mat);
		        return View(data);
			}
			catch (FaultException e)
	        {
		        ms.Abort();
		        ViewBag.ErrorMessage = e.Message;
		        return View();
	        }
	        catch (Exception e)
	        {
		        ms.Abort();
		        ViewBag.ErrorMessage = e.Message;
		        return View();
	        }
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
			catch (FaultException e)
            {
	            ms.Abort();
	            ViewBag.ErrorMessage = e.Message;
	            return View();
            }
            catch (Exception e)
            {
	            ms.Abort();
	            ViewBag.ErrorMessage = e.Message;
	            return View();
            }
		}

        [HttpPost]
        public JsonResult SearchByName(string name)
        {
            return Json(ms.Search(name), JsonRequestBehavior.AllowGet);
        }

        public ActionResult MyMaterials(User user)
        {
	        try
	        {
				user.Id = HttpContext.User.Identity.GetUserId();
				var data = ms.ReadAllMaterialsForUser(user);
				return View(data);
	        }
			catch (FaultException e)
	        {
		        ms.Abort();
		        ViewBag.ErrorMessage = e.Message;
		        return View();
	        }
	        catch (Exception e)
	        {
		        ms.Abort();
		        ViewBag.ErrorMessage = e.Message;
		        return View();
	        }
        }
    }
}
