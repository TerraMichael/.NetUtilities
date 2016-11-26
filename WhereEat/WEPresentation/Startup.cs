using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WEPresentation.Startup))]
namespace WEPresentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
