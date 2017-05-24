using System;
using System.ServiceModel;
using System.Web.Mvc;
using BookAnArtisanMVC.ServiceReference;
using Microsoft.AspNet.Identity;

namespace BookAnArtisanMVC.Controllers
{
	public class MaterialsController : Controller
	{
		private readonly MaterialServiceClient materialServiceClient = new MaterialServiceClient();

		// GET: Material
		public ActionResult Index()
		{
			try
			{
				var data = materialServiceClient.ReadAllMaterial();
				return View(data);
			}
			catch (FaultException e)
			{
				materialServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
			catch (Exception e)
			{
				materialServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
		}

		// GET: Material/Details/5
		public ActionResult Details(Material mat)
		{
			try
			{
				var data = materialServiceClient.ReadMaterial(mat);
				return View(data);
			}
			catch (FaultException e)
			{
				materialServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
			catch (Exception e)
			{
				materialServiceClient.Abort();
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
				materialServiceClient.CreateMaterial(mat);
				return RedirectToAction("Index");
			}
			catch (FaultException e)
			{
				materialServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View(mat);
			}
			catch (Exception e)
			{
				materialServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View(mat);
			}
		}

		// GET: Material/Edit/5
		public ActionResult Edit(Material mat)
		{
			var data = materialServiceClient.ReadMaterial(mat);
			return View(data);
		}

		// POST: Material/Edit/5
		[HttpPost, ActionName("Edit")]
		public ActionResult EditConfirmed(Material mat)
		{
			try
			{
				materialServiceClient.UpdateMaterial(mat);
				return RedirectToAction("Index");
			}
			catch (FaultException e)
			{
				materialServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View(mat);
			}
			catch (Exception e)
			{
				materialServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View(mat);
			}
		}

		// GET: Material/Delete/5
		public ActionResult Delete(Material mat)
		{
			try
			{
				var data = materialServiceClient.ReadMaterial(mat);
				return View(data);
			}
			catch (FaultException e)
			{
				materialServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
			catch (Exception e)
			{
				materialServiceClient.Abort();
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
				materialServiceClient.DeleteMaterial(mat);
				return RedirectToAction("Index");
			}
			catch (FaultException e)
			{
				materialServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
			catch (Exception e)
			{
				materialServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
		}

		[HttpPost]
		public JsonResult SearchByName(string name)
		{
			return Json(materialServiceClient.Search(name), JsonRequestBehavior.AllowGet);
		}

		public ActionResult MyMaterials(User user)
		{
			try
			{
				user.Id = HttpContext.User.Identity.GetUserId();
				var data = materialServiceClient.ReadAllMaterialsForUser(user);
				return View(data);
			}
			catch (FaultException e)
			{
				materialServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
			catch (Exception e)
			{
				materialServiceClient.Abort();
				ViewBag.ErrorMessage = e.Message;
				return View();
			}
		}
	}
}
