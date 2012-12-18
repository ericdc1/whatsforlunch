using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Lunch.Core.Helpers;
using Lunch.Core.Models;
using Lunch.Website.DependencyResolution;
using Quartz;
using Quartz.Impl;
using StackExchange.Profiling;
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

            new Lunch.Core.Helpers.JobScheduler().Taskmanager();

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());

            CreateMaps();
        }


        private static void CreateMaps()
        {
            Mapper.CreateMap<Restaurant, ViewModels.Restaurant>();
            Mapper.CreateMap<ViewModels.Restaurant, Restaurant>();
        }

        private void Application_BeginRequest()
        {
            StackExchange.Profiling.MiniProfiler.Start();
            StackExchange.Profiling.MiniProfiler.Settings.PopupRenderPosition = RenderPosition.Right;    
        }

        private void Application_EndRequest()
        {
            StackExchange.Profiling.MiniProfiler.Stop();
        }
    }
}

