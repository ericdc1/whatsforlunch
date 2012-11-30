using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Lunch.Website.Models;
using Quartz;
using Quartz.Impl;

namespace Lunch.Website.Helpers
{
    public class JobScheduler
    {
        public void Taskmanager()
        {
            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            IJobDetail jobDetail1 = JobBuilder.Create<RecurringJob>()
                .WithIdentity("recurringtrigger", "group1")
                .Build();
            ITrigger recurringtrigger = TriggerBuilder
                .Create()
                .WithIdentity("recurringtrigger", "group1")
                .StartAt(DateBuilder.FutureDate(5, IntervalUnit.Second))
                .WithSimpleSchedule(x => x.WithInterval(new TimeSpan(0, 0, 1, 0))
                .RepeatForever())
                .Build();
            sched.ScheduleJob(jobDetail1, recurringtrigger);
        }
    }

    public class RecurringJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var functionarray = new string[3] { "Test1", "Test2", "Test3" };
            var random = new Random();
            var index = random.Next(0, functionarray.Length);
            var functiontorun = functionarray[index];
            var calledType = Type.GetType("Lunch.Website.Helpers.Jobs");
            var settings = "{ 'firstName':'John' , 'lastName':'Doe' }";
            if (calledType != null)
                calledType.InvokeMember(functiontorun, BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, new object[] {settings});
        }

    }

    public class Settings
    {
        public string Settingsjson { get; set; }
    }

}