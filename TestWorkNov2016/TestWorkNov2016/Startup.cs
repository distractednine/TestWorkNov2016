using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TestWorkNov2016.Startup))]

namespace TestWorkNov2016
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}