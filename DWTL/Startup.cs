using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DWTL.Startup))]
namespace DWTL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
