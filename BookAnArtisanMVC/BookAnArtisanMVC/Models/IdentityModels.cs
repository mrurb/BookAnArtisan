﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookAnArtisanMVC.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }


		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            //userIdentity.AddClaims(new[]
            //{
            //    new Claim("MyApp:FirstName", FirstName),
            //    new Claim("MyApp:LastName", LastName),
            //    new Claim("MyApp:Phone", Phone),
            //    new Claim("MyApp:Address", Address)
            //});
            return userIdentity;
        }
    }

	public class ApplicationRole : IdentityRole
	{
		
	}
}