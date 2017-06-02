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
		readonly ApplicationDbContext dbcontext = new ApplicationDbContext();
		private ApplicationUserManager userManager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

		public ApplicationUsersController()
		{
			
		}
		public ApplicationUsersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
		{
			this.userManager = userManager;
		}


		private ApplicationUserManager UserManager
		{
			get { return userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
			set { userManager = value; }
		}

		[Authorize]
		// GET: ApplicationUsers
		public ActionResult Index()
		{
			return View(dbcontext.Users.ToList());
		}

		[Authorize]
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

		[Authorize]
		// GET: ApplicationUsers/Create
		public ActionResult Create()
		{
			var model = new CreateUserViewModel();
			return View(model);
		}

		[Authorize]
		// POST: ApplicationUsers/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Email, Password")] CreateUserViewModel model)
		{
			var user = new ApplicationUser {UserName = model.Email, Email = model.Email};
			var result = await UserManager.CreateAsync(user, model.Password);
			if (result.Succeeded)
			{
				return RedirectToAction("Index");
			}

			return View(model);
		}

		[Authorize]
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

		[Authorize]
		// POST: ApplicationUsers/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "FirstName,LastName,PhoneNumber,Address,Email")] EditUserViewModel model)
		{
			var user = new ApplicationUser {UserName = model.Email, Email = model.Email, FirstName = model.FirstName, PhoneNumber = model.PhoneNumber};
			var result = await UserManager.UpdateAsync(user);
			if (result.Succeeded)
			{
				return RedirectToAction("Index");
			}

			return View(model);
		}

		[Authorize]
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

		[Authorize]
		// POST: ApplicationUsers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(string id)
		{
			ApplicationUser applicationUser = userManager.FindById(id);
			var result = await userManager.DeleteAsync(applicationUser);
			return RedirectToAction("Index");
		}

		public ActionResult AddRoleToUser(string id)
		{
			var roleList = dbcontext.IdentityRoles.ToList();
			ViewBag.list = roleList;
			var model = new AddRoleToUserViewModel() {UserId = id};
			return View(model);
		}

		[HttpPost]
		public async Task<ActionResult> AddRoleToUser(AddRoleToUserViewModel model)
		{
			var result = await userManager.AddToRoleAsync(model.UserId, model.Role.Name);
			if (result.Succeeded)
			{
				RedirectToAction("Index");
			}
			var roleList = dbcontext.IdentityRoles.ToList();
			ViewBag.list = roleList;
			ViewBag.ErrorMessage = result.Errors.First();
			return View(model);
		}
	}
}