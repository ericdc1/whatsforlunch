using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lunch.Core.Logic;
using Lunch.Core.Models;
using Quartz;
using Quartz.Impl;
using StructureMap;

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
            try
            {
                
            //Check to see if today is a lunch day
                if (Helpers.IsLunchDate(Helpers.AdjustTimeOffsetFromUtc(DateTime.UtcNow)))
                {
                    //add log
                    var _jobLogLogic = ObjectFactory.GetInstance<IJobLogLogic>();
                    var entity = new JobLog() {JobID = 0, Category = "System", Message = "Running recurring job"};
                    _jobLogLogic.SaveOrUpdate(entity);

                    var _jobLogic = ObjectFactory.GetInstance<IJobLogic>(); 
                    var jobsfortoday =
                        _jobLogic.GetAll()
                                 .Where(f => f.RunDate.ToShortDateString() == DateTime.UtcNow.ToShortDateString())
                                 .ToList();

                    //Check to see if there are jobs in the db for today
                    //if not add jobs
                    if (jobsfortoday.Count == 0)
                    {
                        new Helpers().CreateJobs();

                        entity = new JobLog() {JobID = 0, Category = "System", Message = "Creating Jobs"};
                        _jobLogLogic.SaveOrUpdate(entity);
                    }

                    //check to see if any tasks exist that need to run now
                    //run those jobs and log actions
                    //Todo: should create a logic method to get all that has not run
                    var jobstorun = _jobLogic.GetAll().Where(f => f.RunDate < DateTime.UtcNow && f.HasRun == false);

                    foreach (var job in jobstorun)
                    {
                        ObjectFactory.GetInstance<Helpers>().RunJob(job.MethodName, job.ParametersJson, job.Id);
                        job.HasRun = true;
                        _jobLogic.SaveOrUpdate(job);

                        entity = new JobLog()
                                     {
                                         JobID = 0,
                                         Category = "System",
                                         Message = string.Format("Running Job {0}", job.MethodName)
                                     };
                        _jobLogLogic.SaveOrUpdate(entity);
                    }
                }
               
            }
            catch (Exception ex)
            {
                var _jobLogLogic = ObjectFactory.GetInstance<IJobLogLogic>();
                var entity = new JobLog()
                {
                    JobID = 0,
                    Category = "Error",
                    Message = string.Format("Error in job execution: {0}", ex.Message)
                };
                _jobLogLogic.SaveOrUpdate(entity);
            }
        }
    }
}