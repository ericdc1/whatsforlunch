﻿using System;
using System.Configuration;
using System.Diagnostics;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Lunch.Core.Jobs;
using Lunch.Core.Models;
using Lunch.Website.Controllers;
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


        public static bool SetupComplete;

        protected void Application_Start()
        {

           SetupComplete = Convert.ToBoolean(ConfigurationManager.AppSettings["SetupComplete"]);
           Application["SetupComplete"]  = SetupComplete;

            ObjectFactory.Initialize(i => i.AddRegistry<StructureMapRegistry>());
            ObjectFactory.AssertConfigurationIsValid();
    
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            new JobScheduler().Taskmanager();

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
            
            CreateMaps();
        }


        private static void CreateMaps()
        {
            Mapper.CreateMap<Restaurant, ViewModels.Restaurant>();
            Mapper.CreateMap<ViewModels.Restaurant, Restaurant>();
            Mapper.CreateMap<User, ViewModels.User>();
            Mapper.CreateMap<ViewModels.User, User>();
            Mapper.CreateMap<User, ViewModels.UserCreate>();
            Mapper.CreateMap<ViewModels.UserCreate, User>();
            Mapper.CreateMap<User, ViewModels.UserEdit>();
            Mapper.CreateMap<ViewModels.UserEdit, User>();
            Mapper.CreateMap<ViewModels.UserManage, User>();
            Mapper.CreateMap<User, ViewModels.UserManage>();
            Mapper.CreateMap<ViewModels.RestaurantType, RestaurantType>();
            Mapper.CreateMap<RestaurantType, ViewModels.RestaurantType>();
        }

        private void Application_BeginRequest()
        {
            
            if (!SetupComplete && !Request.Url.AbsolutePath.Contains("setupwizard"))
            {
                Response.Status = "301 Moved Permanently";
                Response.AddHeader("Location", "/setupwizard");
                Response.End();
            }

            StackExchange.Profiling.MiniProfiler.Start();
            StackExchange.Profiling.MiniProfiler.Settings.PopupRenderPosition = RenderPosition.Right;    
        }

        private void Application_EndRequest()
        {
            StackExchange.Profiling.MiniProfiler.Stop();
        }

	//TODO: try this approach
        //http://stackoverflow.com/questions/619895/how-can-i-properly-handle-404-in-asp-net-mvc
        //protected void Application_Error()
        //{
        //    var exception = Server.GetLastError();
        //    var httpException = exception as HttpException;
        //    Response.Clear();
        //    Server.ClearError();
        //    var routeData = new RouteData();
        //    routeData.Values["controller"] = "Errors";
        //    routeData.Values["action"] = "General";
        //    routeData.Values["exception"] = exception;
        //    Response.StatusCode = 500;
        //    if (httpException != null)
        //    {
        //        Response.StatusCode = httpException.GetHttpCode();
        //        switch (Response.StatusCode)
        //        {
        //            case 403:
        //                routeData.Values["action"] = "Http403";
        //                break;
        //            case 404:
        //                routeData.Values["action"] = "Http404";
        //                break;
        //        }
        //    }

        //    IController errorsController = new ErrorsController();
        //    var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
        //    errorsController.Execute(rc);
        //}

    }
}

