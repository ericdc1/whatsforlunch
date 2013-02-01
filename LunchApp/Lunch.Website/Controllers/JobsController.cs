using System.Linq;
using System.Web.Mvc;
using Lunch.Core.Logic;
using Lunch.Website.Services;
using PagedList;

namespace Lunch.Website.Controllers
{
    [LunchAuthorize(Roles = "Administrator")]
    public class JobsController : BaseController
    {
        private readonly IJobLogic _jobLogic;
       
        public JobsController(IJobLogic jobLogic)
        {
            _jobLogic = jobLogic;
        }


        public ActionResult Index(int? categoryid, int? page)
        {
            ViewBag.HasCategoryFilter = categoryid > 0;
            var pageNumber = page ?? 1;
            var jobs = _jobLogic.GetAll().Take(50).OrderByDescending(f => f.Id);
            var onePageofJobs = jobs.ToPagedList(pageNumber, 25);

            //var result = _jobLogic.GetAll().Take(50).OrderByDescending(f=>f.Id);
            return View(onePageofJobs);
        }

    }
}
