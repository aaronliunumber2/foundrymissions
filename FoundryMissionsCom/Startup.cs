using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FoundryMissionsCom.Startup))]
namespace FoundryMissionsCom
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
