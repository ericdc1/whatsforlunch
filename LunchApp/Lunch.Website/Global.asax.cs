using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Lunch.Core.Helpers;
using Lunch.Core.Models;
using Lunch.Website.DependencyResolution;
using Quartz;
using Quartz.Impl;
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


//public class go
//{
//    public void RunJob(string methodname, string parameters)
//    {
//        var calledType = Assembly.Load("Lunch.Core").GetType("Lunch.Core.Helpers.Jobs");
//        if (calledType != null)
//        {
//            var methods = calledType.GetMethods(BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public);
//            foreach (var method in methods)
//            {
//                if (method.Name == methodname)
//                {
//                    object jobsInstance =  ObjectFactory.GetInstance(calledType);
//                    calledType.InvokeMember(methodname, BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, jobsInstance, new object[] { parameters });
//                    break;
//                }
//            }
//        }
//        else
//        {
//            //ToDo - email or log that this failed
//        }
//    }
//}

//     public class JobScheduler
//    {
//        public void Taskmanager()
//        {
//            // construct a scheduler factory
//            ISchedulerFactory schedFact = new StdSchedulerFactory();

//            // get a scheduler
//            IScheduler sched = schedFact.GetScheduler();
//            sched.Start();

//            IJobDetail jobDetail1 = JobBuilder.Create<RecurringJob>()
//                .WithIdentity("recurringtrigger", "group1")
//                .Build();
//            ITrigger recurringtrigger = TriggerBuilder
//                .Create()
//                .WithIdentity("recurringtrigger", "group1")
//                .StartAt(DateBuilder.FutureDate(5, IntervalUnit.Second))
//                .WithSimpleSchedule(x => x.WithInterval(new TimeSpan(0, 0, 1, 0))
//                .RepeatForever())
//                .Build();
//            sched.ScheduleJob(jobDetail1, recurringtrigger);
//        }
//    }

//    public class RecurringJoba : IJob
    //{
    //    public void Execute(IJobExecutionContext context)
    //    {
    //        //Check to see if today is a lunch day
    //        if (Helpers.IsLunchDate(DateTime.Now))
    //        {
    //            //Check to see if there are jobs in the db for today
    //            //if not add jobs
    //            //ToDo: get this from the db
    //            var jobsfortoday = new List<Job>();
    //            if (jobsfortoday.Count == 0)
    //            {
    //                //new Helpers().CreateJobs(DateTime.Now);
    //            }

    //            //check to see if any tasks exist that need to run now
    //            //run those jobs and log actions
    //            //Todo: get list of jobs that are in today's date and before NOW that have not been flagged as ran
    //            var jobstorun = new List<Job>();
    //            jobstorun.Add(new Job() { CreatedDate = DateTime.Now, MethodName = "MorningMessage", ParametersJson = "{name:bob}", RunDate=DateTime.Now.AddMinutes(-5)});
    //            foreach (var job in jobstorun)
    //            {
    //               new go().RunJob(job.MethodName, job.ParametersJson);
    //            }

    //        }


    //    }


   