using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RpshopingMvc.Startup))]
namespace RpshopingMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
