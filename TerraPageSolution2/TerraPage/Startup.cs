using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TerraPage.Startup))]
namespace TerraPage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
