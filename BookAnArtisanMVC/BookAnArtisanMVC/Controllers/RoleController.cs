using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookAnArtisanMVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookAnArtisanMVC.Controllers
{
    public class RoleController : Controller
    {
	    private readonly ApplicationDbContext context;

	    public RoleController()
	    {
		    this.context = new ApplicationDbContext();
	    }

	    // GET: Role
        public ActionResult Index()
        {
	        var roles = context.Roles.ToList();
            return View(roles);
        }

	    public ActionResult Create()
	    {
		    var role = new IdentityRole();
		    return View(role);
	    }

	    [HttpPost]
	    public ActionResult Create(IdentityRole role)
	    {
		    context.Roles.Add(role);
		    context.SaveChanges();
		    return RedirectToAction("Index");
	    }
	}
}