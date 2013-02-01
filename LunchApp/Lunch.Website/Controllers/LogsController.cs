using System;
using System.Linq;
using System.Web.Mvc;
using Lunch.Core.Logic;
using Lunch.Core.Models;
using Lunch.Website.Services;
using PagedList;

namespace Lunch.Website.Controllers
{
    [LunchAuthorize(Roles = "Administrator")]
    public class LogsController : BaseController
    {
        private readonly IJobLogLogic _jobLogLogic;
       
        public LogsController(IJobLogLogic jobLogLogic)
        {
            _jobLogLogic = jobLogLogic;
        }

        public ActionResult Index(int? categoryid, int? page)
        {
            var newlog = new JobLog() {Category = "mycat", CreatedAt = DateTime.Now, JobId = 1, Message = "Created"};
            _jobLogLogic.SaveOrUpdate(newlog);

            ViewBag.HasCategoryFilter = categoryid > 0;
            var pageNumber = page ?? 1;
            var logs = _jobLogLogic.GetAll().OrderByDescending(f => f.JobLogId).Take(250);
            var onePageofLogs = logs.ToPagedList(pageNumber, 25);

            //var result = _jobLogLogic.GetAll().OrderByDescending(f=>f.JobLogId).Take(250);
            return View(onePageofLogs);
        }
    }
}
