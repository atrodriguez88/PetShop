using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PetWebApp.Startup))]
namespace PetWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
