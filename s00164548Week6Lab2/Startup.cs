using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(s00164548Week6Lab2.Startup))]
namespace s00164548Week6Lab2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
