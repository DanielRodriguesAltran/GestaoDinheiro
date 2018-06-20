using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GestaoDinheiro.Startup))]
namespace GestaoDinheiro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
