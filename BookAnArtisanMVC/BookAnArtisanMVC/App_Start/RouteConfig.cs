using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookAnArtisanMVC
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Users", // Route name
				url: "Users/page/{page}", // URL with parameters
				defaults: new { controller = "User", action = "List", page = 1 }
			);
			/*
			routes.MapRoute(
				"RouteByUser",
				"User/Page/{page}",
				new { controller = "UserController", action = "List" }
			);

			routes.MapRoute(
				"RouteByUserFirstPage",
				"User",
				new { controller = "UserController", action = "List", page = 1 }
			);
			*/
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);


		}
	}
}
