using Microsoft.AspNet.Identity.EntityFramework;

namespace BookAnArtisanMVC.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}

		public System.Data.Entity.DbSet<BookAnArtisanMVC.Models.ApplicationRole> IdentityRoles { get; set; }
	}
}