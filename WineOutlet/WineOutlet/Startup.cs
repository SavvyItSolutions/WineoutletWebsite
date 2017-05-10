using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WineOutlet.Startup))]
namespace WineOutlet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
