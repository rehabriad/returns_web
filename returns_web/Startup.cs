using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(returns_web.Startup))]
namespace returns_web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
