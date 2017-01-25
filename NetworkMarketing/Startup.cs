using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NetworkMarketing.Startup))]
namespace NetworkMarketing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
