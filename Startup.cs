using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyFirstApp.Startup))]
namespace MyFirstApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
