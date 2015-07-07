using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BulkSMSWebApp.Startup))]
namespace BulkSMSWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
