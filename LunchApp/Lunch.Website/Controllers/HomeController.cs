using System;
using System.Globalization;
using System.Web.Mvc;
using Lunch.Core.Helpers;
using Lunch.Core.Logic;
using Lunch.Core.Models;

namespace Lunch.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRestaurantLogic _restaurantLogic;

        public HomeController(IRestaurantLogic restaurantLogic)
        {
            _restaurantLogic = restaurantLogic;
        }

        //
        // GET: /Home/
        
        public ActionResult Index()
        {
            return View();            
        }

        public ActionResult Keepalive()
        {
            return Content("alive");
        }

    }
}
