using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookAnArtisanMVC.Startup))]
namespace BookAnArtisanMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
