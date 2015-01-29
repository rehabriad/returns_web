using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ST.WebUI.Startup))]
namespace ST.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
