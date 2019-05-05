using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DistDuties.Startup))]
namespace DistDuties
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
