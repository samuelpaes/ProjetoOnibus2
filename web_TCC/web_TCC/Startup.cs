using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LiveBus.Startup))]
namespace LiveBus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
