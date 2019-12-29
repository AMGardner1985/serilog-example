using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TodoForms.Startup))]
namespace TodoForms
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
