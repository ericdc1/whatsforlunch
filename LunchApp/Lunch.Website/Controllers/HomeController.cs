using System.Web.Mvc;

namespace Lunch.Website.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        
        public ActionResult Index()
        {
            //var model = new JobLogRepository().GetList().OrderByDescending(f=>f.LogDTM).Take(20);
            return View();
            
        }

        public ActionResult Keepalive()
        {
            return Content("alive");
        }

    }
}
