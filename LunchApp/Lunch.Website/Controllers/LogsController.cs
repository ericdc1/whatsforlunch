using System;
using System.Linq;
using System.Web.Mvc;
using Lunch.Core.Logic;
using Lunch.Core.Models;
using Lunch.Website.Services;

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

        public ActionResult Index(int? categoryid)
        {

            var newlog = new JobLog() {Category = "mycat", CreatedAt = DateTime.Now, JobId = 1, Message = "Created"};
            _jobLogLogic.SaveOrUpdate(newlog);

            ViewBag.HasCategoryFilter = categoryid > 0;
            var result = _jobLogLogic.GetAll().OrderByDescending(f=>f.JobLogId).Take(250);
            return View(result);
        }
    }
}
