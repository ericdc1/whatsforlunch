using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Web.Mvc;
using Lunch.Core.Logic;
using Lunch.Core.Models;
using Lunch.Data.Repositories;
using Lunch.Website.Services;
using Lunch.Website.ViewModels;
using StackExchange.Exceptional;
using Lunch.Core.Jobs;
using StructureMap;
using RazorEngine;
using System.IO;
using Restaurant = Lunch.Website.ViewModels.Restaurant;
using User = Lunch.Website.ViewModels.User;

namespace Lunch.Website.Controllers
{
    public class HomeController : BaseController
    {
       
        private readonly IRestaurantLogic _restaurantLogic;
        private readonly IRestaurantTypeLogic _restaurantTypeLogic;
        private readonly IVoteLogic _voteLogic;


        public DateTime Overridetime = new DateTime(2013, 2, 1, 14, 16, 0);


        public HomeController(IRestaurantLogic restaurantLogic, IRestaurantTypeLogic restaurantTypeLogic, IVoteLogic voteLogic)
        {
            _restaurantLogic = restaurantLogic;
            _restaurantTypeLogic = restaurantTypeLogic;
            _voteLogic = voteLogic;
        }


        public ActionResult Index()
        {
            var model = new Homepage();
            model.RestaurantsForToday = _restaurantLogic.GetSelection().ToList();
            model.YourVote = _voteLogic.GetItem(CurrentUser.Id, Helpers.AdjustTimeOffsetFromUtc(DateTime.UtcNow));
            model.PeopleWhoVotedToday = new List<Core.Models.User>(); // TODO: Call the method to populate this.
            if (model.YourVote != null)
            {
                model.YourVote.Restaurant = _restaurantLogic.Get(model.YourVote.RestaurantId);
            }
            var currenttime = Overridetime;   //Lunch.Core.Jobs.Helpers.AdjustTimeOffsetFromUtc(DateTime.UtcNow));
            
            return RedirectCheck(model, currenttime);
        }

        public ActionResult Keepalive()
        {
            return Content("alive");
        }

        public ActionResult SaveVote(int id)
        {
            var vote = _voteLogic.SaveVote(id, CurrentUser.Id);
            return RedirectToAction("Index");
        }


        /// <summary>
        /// This lets you access the error handler via a route in your application, secured by whatever
        /// mechanisms are already in place.
        /// </summary>
        /// <remarks>If mapping via RouteAttribute: [Route("errors/{path?}/{subPath?}")]</remarks>
        public ActionResult Exceptions()
        {
            var context = System.Web.HttpContext.Current;
            var page = new HandlerFactory().GetHandler(context, Request.RequestType, Request.Url.ToString(), Request.PathInfo);
            page.ProcessRequest(context);

            return null;
        }

        [NonAction]
        private ActionResult RedirectCheck(Homepage model, DateTime currenttime)
        {
            if (currenttime < new DateTime(currenttime.Year, currenttime.Month, currenttime.Day, 7, 30, 0))
            {
                return View("notyet", model);
            }

            if (currenttime > new DateTime(currenttime.Year, currenttime.Month, currenttime.Day, 7, 30, 0) && currenttime < new DateTime(currenttime.Year, currenttime.Month, currenttime.Day, 10, 30, 0))
            {
                return View(model.YourVote == null ? "vote" : "voted", model);
            }


            if (currenttime > new DateTime(currenttime.Year, currenttime.Month, currenttime.Day, 10, 30, 0) && currenttime < new DateTime(currenttime.Year, currenttime.Month, currenttime.Day, 11, 15, 0))
            {
                return View(model.YourVote == null ? "comebacktomorrow" : "voted", model);
                // If you have overrides you can override otherwise show results.
            }

            if (currenttime > new DateTime(currenttime.Year, currenttime.Month, currenttime.Day, 11, 15, 0) && currenttime < new DateTime(currenttime.Year, currenttime.Month, currenttime.Day, 14, 0, 0))
            {
                return View("going", model);
            }

            if (currenttime > new DateTime(currenttime.Year, currenttime.Month, currenttime.Day, 14, 0, 0))
            {
                return View(model.YourVote == null ? "comebacktomorrow" : "Rate", model);
            }
            return null;
        }

    }
}
