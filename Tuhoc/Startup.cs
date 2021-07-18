using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tuhoc.Startup))]
namespace Tuhoc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
