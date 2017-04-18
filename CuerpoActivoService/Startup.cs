using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CuerpoActivoService.Startup))]

namespace CuerpoActivoService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application using OWIN startup, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();
        }
    }
}
