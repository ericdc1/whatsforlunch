using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lunch.Core.Logic;
using Lunch.Core.Models;

namespace Lunch.Website.Controllers
{
    public class HistoryController : BaseController
    {
        private readonly IRestaurantOptionLogic _restaurantOptionLogic;

        public HistoryController(IRestaurantOptionLogic restaurantOptionLogic)
        {
            _restaurantOptionLogic = restaurantOptionLogic;
        }

        public ActionResult Index()
        {
            var restaurantoptionlist = new List<IEnumerable<RestaurantOption>>();
            var startdate = DateTime.Now.AddDays(-30);
            var enddate = DateTime.Now;

            while (startdate <= enddate)
            {
                restaurantoptionlist.Add(_restaurantOptionLogic.GetAllByDate(startdate));
                startdate = startdate.AddDays(1);
            }
                //= 
            return View(restaurantoptionlist);
        }


    }
}
