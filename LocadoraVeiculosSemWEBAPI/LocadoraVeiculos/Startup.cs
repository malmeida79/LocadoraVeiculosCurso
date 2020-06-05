using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LocadoraVeiculos.Startup))]
namespace LocadoraVeiculos
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
