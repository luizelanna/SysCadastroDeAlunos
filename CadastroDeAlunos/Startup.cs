using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CadastroDeAlunos.Startup))]
namespace CadastroDeAlunos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
