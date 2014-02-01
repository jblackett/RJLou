using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RJLou.Startup))]
namespace RJLou
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
