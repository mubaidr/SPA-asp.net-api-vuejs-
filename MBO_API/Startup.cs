using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MBO_API.Startup))]

namespace MBO_API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
