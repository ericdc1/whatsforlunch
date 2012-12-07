using System;
using System.Globalization;
using System.Web.Mvc;
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
            _restaurantLogic.SaveOrUpdate(new Restaurant() {RestaurantName = "My restaurant" });
            var allrest = _restaurantLogic.Get(f => f.ID < 3);
            foreach(var res in allrest)
            {
                res.RestaurantName = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            }
            var model = _restaurantLogic.GetAll();
            return View(model);            
        }

        public ActionResult Keepalive()
        {
            return Content("alive");
        }

    }
}
