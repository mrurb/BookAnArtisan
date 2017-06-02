using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BookAnArtisanMVC.Models;

namespace BookAnArtisanMVC.Controllers
{
    public class ApplicationRolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicationRoles
        public ActionResult Index()
        {
            return View(db.IdentityRoles.ToList());
        }

        // GET: ApplicationRoles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole applicationRole = db.IdentityRoles.Find(id);
            if (applicationRole == null)
            {
                return HttpNotFound();
            }
            return View(applicationRole);
        }

        // GET: ApplicationRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplicationRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] ApplicationRole applicationRole)
        {
            if (ModelState.IsValid)
            {
                db.IdentityRoles.Add(applicationRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationRole);
        }

        // GET: ApplicationRoles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole applicationRole = db.IdentityRoles.Find(id);
            if (applicationRole == null)
            {
                return HttpNotFound();
            }
            return View(applicationRole);
        }

        // POST: ApplicationRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] ApplicationRole applicationRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationRole);
        }

        // GET: ApplicationRoles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole applicationRole = db.IdentityRoles.Find(id);
            if (applicationRole == null)
            {
                return HttpNotFound();
            }
            return View(applicationRole);
        }

        // POST: ApplicationRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationRole applicationRole = db.IdentityRoles.Find(id);
            db.IdentityRoles.Remove(applicationRole);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
