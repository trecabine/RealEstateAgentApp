using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RealEstateAgencyApp.Startup))]
namespace RealEstateAgencyApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
