using System;
using System.Collections.Generic;
using Lunch.Core.Logic;
using Lunch.Core.Models;
using Quartz;
using Quartz.Impl;
using StructureMap;

namespace Lunch.Website.Scheduler
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
            //Check to see if today is a lunch day
            if (Website.Scheduler.Helpers.IsLunchDate(DateTime.Now))
            {
                //Check to see if there are jobs in the db for today
                //if not add jobs
                //ToDo: get this from the db
                var jobsfortoday = new List<Job>();
                if (jobsfortoday.Count == 0)
                {
                    new Website.Scheduler.Helpers().CreateJobs();
                }

                //check to see if any tasks exist that need to run now
                //run those jobs and log actions
                //Todo: get list of jobs that are in today's date and before NOW that have not been flagged as ran
                var _jobLogic = ObjectFactory.GetInstance<IJobLogic>();
                var jobstorun = _jobLogic.Get(f => f.RunDate < DateTime.Now && f.HasRun == false);
                //jobstorun.Add(new Job() { CreatedDate = DateTime.Now, MethodName = "MorningMessage", ParametersJson = "{name:bob}", RunDate=DateTime.Now.AddMinutes(-5)});
                foreach (var job in jobstorun)
                {
                    ObjectFactory.GetInstance<Website.Scheduler.Helpers>().RunJob(job.MethodName, job.ParametersJson);
                }

            }
        }
    }
}