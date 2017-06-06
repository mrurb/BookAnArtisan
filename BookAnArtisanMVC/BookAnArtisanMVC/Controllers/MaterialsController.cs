using System;
using System.ServiceModel;
using System.Web.Mvc;
using BookAnArtisanMVC.Models;
using BookAnArtisanMVC.ServiceReference;
using Microsoft.AspNet.Identity;

namespace BookAnArtisanMVC.Controllers
{
	public class MaterialsController : Controller
	{
		private readonly MaterialServiceClient materialServiceClient = new MaterialServiceClient();

		// GET: Material
		public ActionResult Index(int? page)
		{
			try
			{
				var viewModel = new IndexViewModel<Material>
				{
					Pager = materialServiceClient.ReadMaterialPage(page,null)
				};
				return View(viewModel);
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
		[Authorize]
		public ActionResult Create()
		{
			return View();
		}

		// POST: Material/Create
		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
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
		[Authorize]
		public ActionResult Edit(Material mat)
		{
			if (mat.Id == 0)
			{
				return RedirectToAction("Index");
			}
			var data = materialServiceClient.ReadMaterial(mat);
			return View(data);
		}

		// POST: Material/Edit/5
		[Authorize]
		[HttpPost, ActionName("Edit")]
		[ValidateAntiForgeryToken]
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
		[Authorize]
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
		[Authorize]
		[ValidateAntiForgeryToken]
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

		public ActionResult MyMaterials(int? page)
		{
			try
			{
				var viewModel = new IndexViewModel<Material>
				{
					Pager = materialServiceClient.ReadMaterialPageForUser(HttpContext.User.Identity.GetUserId(),page, null)
				};
				return View(viewModel);
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
