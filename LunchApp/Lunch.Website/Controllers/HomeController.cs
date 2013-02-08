using System;
using System.Linq;
using System.Web.Mvc;
using Lunch.Core.Logic;
using Lunch.Website.ViewModels;
using StackExchange.Exceptional;
using Lunch.Core.Jobs;

namespace Lunch.Website.Controllers
{
    public class HomeController : BaseController
    {
       
        private readonly IRestaurantLogic _restaurantLogic;
        private readonly IRestaurantOptionLogic _restaurantOptionLogic;
        private readonly IVoteLogic _voteLogic;
        private readonly IUserLogic _userLogic;
        private readonly IVetoLogic _vetoLogic;

        public DateTime Overridetime = new DateTime(2013, 2, 1, 11, 00, 0);

        public HomeController(IRestaurantLogic restaurantLogic, IVoteLogic voteLogic, IRestaurantOptionLogic restaurantOptionLogic, IUserLogic userLogic, IVetoLogic vetoLogic)
        {
            _restaurantLogic = restaurantLogic;
            _voteLogic = voteLogic;
            _restaurantOptionLogic = restaurantOptionLogic;
            _userLogic = userLogic;
            _vetoLogic = vetoLogic;
        }

        public ActionResult DeleteVote(int id)
        {
            _voteLogic.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {

           // var _jobLogic = ObjectFactory.GetInstance<Jobs>();
           // _jobLogic.MorningMessage(null, 1);
        

            var model = new Homepage
                {
                    RestaurantsForToday = _restaurantOptionLogic.GetAndSaveOptions().ToList(),
                    YourVote = _voteLogic.GetItem(CurrentUser.Id, Helpers.AdjustTimeOffsetFromUtc(DateTime.UtcNow)),
                    PeopleWhoVotedToday = _userLogic.GetListByVotedDate(null, null).ToList()
                };
            if (model.YourVote != null)
            {
                model.YourVote.Restaurant = _restaurantLogic.Get(model.YourVote.RestaurantId);
            }
            var currenttime =  Lunch.Core.Jobs.Helpers.AdjustTimeOffsetFromUtc(DateTime.UtcNow);
            
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

        public ActionResult SaveVeto()
        {
            var vetos = _vetoLogic.GetAllActiveForUser(CurrentUser.Id);
            var veto = vetos.FirstOrDefault();

            // mark veto as used
            if (veto != null)
            {
                veto.Used = true;
                veto.UsedAt = DateTime.Now;
                _vetoLogic.SaveOrUpdate(veto);
            }
            else
            {
                // no vetos to use
                return RedirectToAction("Index");
            }

            // swap selected on the winner and runner up
            var options = _restaurantOptionLogic.GetAllByDate(DateTime.Now).OrderByDescending(f => f.Votes).Take(2).ToList();
            var winner = options.ElementAt(0);
            winner.Selected = 0;
            _restaurantOptionLogic.SaveOrUpdate(winner);
            var runnerUp = options.ElementAt(1);
            runnerUp.Selected = 1;
            _restaurantOptionLogic.SaveOrUpdate(runnerUp);

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
            if (!Helpers.IsLunchDate(Helpers.AdjustTimeOffsetFromUtc(DateTime.UtcNow)))
            {
                return View("nottoday", model);  
            }
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

                // If you have overrides you can override otherwise show results.
                var unusedvetos = _vetoLogic.GetAllActiveForUser(CurrentUser.Id);
                if (unusedvetos.Any())
                    return View("youcanveto", model);

                return View(model.YourVote == null ? "comebacktomorrow" : "voted", model);
          
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
