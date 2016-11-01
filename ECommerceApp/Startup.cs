using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ECommerceApp.Startup))]
namespace ECommerceApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
