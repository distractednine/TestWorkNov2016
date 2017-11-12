using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;
using Owin;
using Microsoft.Practices.Unity;

[assembly: OwinStartup(typeof(TestWorkNov2016.Startup))]

namespace TestWorkNov2016
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = UnityConfig.GetConfiguredContainer();

            //DependencyResolver.SetResolver(container);
        }
    }
}