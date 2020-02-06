using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorldCuisinesRestaurants.Startup))]
namespace WorldCuisinesRestaurants
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
