using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BookAnArtisanMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BookAnArtisanMVC.Controllers
{
	public class ApplicationUsersController : Controller
	{
		private ApplicationUserManager userManager;


		private ApplicationUserManager UserManager
		{
			get
			{
				return userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			set
			{
				userManager = value;
			}
		}

		// GET: ApplicationUsers
		public async Task<ActionResult> Index()
		{
			return View(await userManager.Users.ToListAsync());
		}

		// GET: ApplicationUsers/Details/5
		public ActionResult Details(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ApplicationUser applicationUser = userManager.FindById(id);
			if (applicationUser == null)
			{
				return HttpNotFound();
			}
			return View(applicationUser);
		}

		// GET: ApplicationUsers/Create
		public ActionResult Create()
		{
			var model = new CreateUserViewModel();
			return View(model);
		}

		// POST: ApplicationUsers/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Email, Password")] CreateUserViewModel model)
		{

			var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
			var result = await UserManager.CreateAsync(user, model.Password);
			if (result.Succeeded)
			{
				return RedirectToAction("Index");
			}

			return View(model);
		}

		// GET: ApplicationUsers/Edit/5
		public ActionResult Edit(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ApplicationUser applicationUser = userManager.FindById(id);
			if (applicationUser == null)
			{
				return HttpNotFound();
			}
			var editUserViewModel = new EditUserViewModel() {Address = applicationUser.Address, Email = applicationUser.Email, FirstName = applicationUser.FirstName, LastName = applicationUser.LastName, PhoneNumber = applicationUser.PhoneNumber};
			return View(editUserViewModel);
		}

		// POST: ApplicationUsers/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "FirstName,LastName,PhoneNumber,Address,Email")] EditUserViewModel model)
		{
			var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, PhoneNumber = model.PhoneNumber};
			var result = await UserManager.UpdateAsync(user);
			if (result.Succeeded)
			{
				return RedirectToAction("Index");
			}

			return View(model);
		}

		// GET: ApplicationUsers/Delete/5
		public ActionResult Delete(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ApplicationUser applicationUser = UserManager.FindById(id);
			if (applicationUser == null)
			{
				return HttpNotFound();
			}
			return View(applicationUser);
		}

		// POST: ApplicationUsers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(string id)
		{
			ApplicationUser applicationUser = userManager.FindById(id);
			var result = await userManager.DeleteAsync(applicationUser);
			return RedirectToAction("Index");
		}
	}
}
