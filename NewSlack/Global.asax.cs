using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using Slack;
using Slack.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewSlack
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            NinjectModule registrations = new NinjectRegistrations();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(new StandardKernel(registrations)));
        }
    }
}
