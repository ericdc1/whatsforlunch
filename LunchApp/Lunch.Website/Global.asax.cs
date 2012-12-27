using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Lunch.Core.Jobs;
using Lunch.Core.Models;
using Lunch.Website.DependencyResolution;
using StackExchange.Profiling;
using StructureMap;


namespace Lunch.Website
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        //private IWebSecurityService _webSecurityService;
        //private IUserLogic _userLogic;

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
            
            CreateMaps();

            //WebSecurity.InitializeDatabaseConnection(ConfigurationManager.ConnectionStrings["AzureSQL"].ConnectionString, "System.Data.SqlServer", "User", "Id", "Email", false);
            //if (!WebSecurity.UserExists("jdehlin@gmail.com"))
            //    WebSecurity.CreateUserAndAccount("jdehlin@gmail.com", "foobar");
        }


        private static void CreateMaps()
        {
            Mapper.CreateMap<Restaurant, ViewModels.Restaurant>();
            Mapper.CreateMap<ViewModels.Restaurant, Restaurant>();
            Mapper.CreateMap<User, ViewModels.User>();
            Mapper.CreateMap<ViewModels.User, User>();
        }

        private void Application_BeginRequest()
        {
            //_webSecurityService = ObjectFactory.GetInstance<IWebSecurityService>();
            //_userLogic = ObjectFactory.GetInstance<IUserLogic>();

            StackExchange.Profiling.MiniProfiler.Start();
            StackExchange.Profiling.MiniProfiler.Settings.PopupRenderPosition = RenderPosition.Right;    
        }

        //private void Application_PostAuthenticateRequest(object sender, EventArgs e)
        //{
        //    var guid = Guid.Empty;
        //    if (!string.IsNullOrWhiteSpace(Request.QueryString.Get("guid")))
        //        guid = Guid.Parse(Request.QueryString.Get("guid"));

        //    if (!_webSecurityService.IsAuthenticated && guid != Guid.Empty)
        //    {
        //        var user = _userLogic.Get(guid);

        //        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(user.Email), new string[] { });
        //    }
        //}

        private void Application_EndRequest()
        {
            StackExchange.Profiling.MiniProfiler.Stop();
        }
    }
}

