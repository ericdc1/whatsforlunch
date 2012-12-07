using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Lunch.Core.Helpers;
using Lunch.Website.DependencyResolution;
using StructureMap;


namespace Lunch.Website
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ObjectFactory.Initialize(i => i.AddRegistry<StructureMapRegistry>());
            ObjectFactory.AssertConfigurationIsValid();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            new JobScheduler().Taskmanager();

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
        }

        private void Application_BeginRequest()
        {
            StackExchange.Profiling.MiniProfiler.Start();
        }

        private void Application_EndRequest()
        {
            StackExchange.Profiling.MiniProfiler.Stop();
        }
    }
}