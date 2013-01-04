using System.Linq;
using System.Web.Mvc;
using Lunch.Core.Logic;

namespace Lunch.Website.Controllers
{
    public class LogsController : BaseController
    {
        private readonly IJobLogLogic _jobLogLogic;
       
        public LogsController(IJobLogLogic jobLogLogic)
        {
            _jobLogLogic = jobLogLogic;
        }

        public ActionResult Index(int? categoryid)
        {
            ViewBag.HasCategoryFilter = categoryid > 0;
            var result = _jobLogLogic.GetAll().OrderByDescending(f=>f.Id).Take(250);
            return View(result);
        }
    }
}
