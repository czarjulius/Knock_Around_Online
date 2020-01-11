using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewKnockaboutonline.Startup))]
namespace NewKnockaboutonline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
