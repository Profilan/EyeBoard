using Microsoft.Owin;
using Owin;


[assembly: OwinStartupAttribute(typeof(EyeBoard.Startup))]
namespace EyeBoard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.MapSignalR();
        }
    }
}
