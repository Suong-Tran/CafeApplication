using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CafeApplication.Startup))]
namespace CafeApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
