using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Lunch.Core.Logic;
using Lunch.Core.Models;

namespace Lunch.Website.Controllers
{
    public class StatsController : Controller
    {
        private readonly IRestaurantLogic _restaurantLogic;
        private readonly IRestaurantOptionLogic _restaurantOptionLogic;
        private readonly IVoteLogic _voteLogic;


        public StatsController(IRestaurantLogic restaurantLogic, IVoteLogic voteLogic, IRestaurantOptionLogic restaurantOptionLogic)
        {
            _restaurantLogic = restaurantLogic;
            _voteLogic = voteLogic;
            _restaurantOptionLogic = restaurantOptionLogic;
        }


        //
        // GET: /Stats/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Stats/RestaurantRatings

        public PartialViewResult RestaurantRatings()
        {
            var results = new ViewModels.Stats.RestaurantRatings
                              {
                                  TopTen = Mapper.Map<List<Restaurant>, List<ViewModels.Restaurant>>(_restaurantLogic.GetTopByRating().ToList()),
                                  TopTenWeighted = Mapper.Map<List<Restaurant>, List<ViewModels.Restaurant>>(_restaurantLogic.GetTopByWeightedRating().ToList()),
                                  RestaurantOptions = _restaurantOptionLogic.GetAll().ToList()
                              };

            return PartialView("_RestaurantRatings", results);
        }

        //
        // GET: /Stats/VoteCharts

        public PartialViewResult VoteCharts()
        {
            var results = new ViewModels.Stats.VoteCharts
                              {
                                  MostResent = _voteLogic.GetItemsForDate(DateTime.Now).ToList()
                              };

            return PartialView("_VoteCharts", results);
        }
    }
}
