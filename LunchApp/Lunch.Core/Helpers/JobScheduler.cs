﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lunch.Core.Models;
using Quartz;
using Quartz.Impl;

namespace Lunch.Core.Helpers
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
            if (Helpers.IsLunchDate(DateTime.Now))
            {
                //Check to see if there are jobs in the db for today
                //if not add jobs
                //ToDo: get this from the db
                var jobsfortoday = new List<Job>();
                if (jobsfortoday.Count == 0)
                {
                    new Helpers().CreateJobs(DateTime.Now);
                }

                //check to see if any tasks exist that need to run now
                //run those jobs and log actions
                //Todo: get list of jobs that are in today's date and before NOW that have not been flagged as ran
                var jobstorun = new List<Job>();
                foreach (var job in jobstorun)
                {
                   new Helpers().RunJob(job.MethodName, job.ParametersJson);
                }

            }
        }
    }
}