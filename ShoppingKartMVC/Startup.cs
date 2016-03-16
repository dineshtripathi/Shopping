using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShoppingKartMVC.Startup))]
namespace ShoppingKartMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
