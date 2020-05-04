using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventFinder.Startup))]
namespace EventFinder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
