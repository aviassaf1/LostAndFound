using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewWebClient.Startup))]
namespace NewWebClient
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
