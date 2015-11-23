using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iMaintenanceTotal.Startup))]
namespace iMaintenanceTotal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
