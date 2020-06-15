using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lab04_2.Startup))]
namespace lab04_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
