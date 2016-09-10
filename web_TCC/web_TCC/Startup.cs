using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(web_TCC.Startup))]
namespace web_TCC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
