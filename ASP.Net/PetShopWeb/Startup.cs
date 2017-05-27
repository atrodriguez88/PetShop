using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PetShopWeb.Startup))]
namespace PetShopWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
