using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lunch.Core.Logic;
using Lunch.Core.Models;

namespace Lunch.Website.Controllers
{
    public class RatingsController : BaseController
    {
        private readonly IRestaurantRatingLogic _restaurantRatingLogic;

        public RatingsController(IRestaurantRatingLogic restaurantRatingLogic)
        {
            _restaurantRatingLogic = restaurantRatingLogic;
        }

        public ActionResult Index()
        {
            return View(_restaurantRatingLogic.GetAllByUser(CurrentUser.Id));
        }

        [HttpPost]
        public ActionResult Save(RestaurantRating model)
        {
            model.UserId = CurrentUser.Id;
            model = _restaurantRatingLogic.Insert(model);
            return new JsonResult() {Data = model};
        }
    }
}
