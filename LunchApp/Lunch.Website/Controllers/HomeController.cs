using System.Web.Mvc;
using Lunch.Core.Logic;

namespace Lunch.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITestLogic _testLogic;

        public HomeController(ITestLogic testLogic)
        {
            _testLogic = testLogic;
        }

        //
        // GET: /Home/
        
        public ActionResult Index()
        {
            return View(_testLogic.GetRestaurants());            
        }

        public ActionResult Keepalive()
        {
            return Content("alive");
        }

    }
}
