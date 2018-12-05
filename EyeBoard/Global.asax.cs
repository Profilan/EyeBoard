using EyeBoard.DependencyResolution;
using EyeBoard.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Profilan.SharedKernel;
using StructureMap;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EyeBoard
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IContainer container = IoC.Initialize();
            DomainEvents.Container = container;
            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => new HubActivator(container));
            
        }
    }
}
