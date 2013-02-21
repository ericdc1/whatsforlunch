using System;
using System.Linq;
using System.Web.Mvc;
using Lunch.Core.Logic;
using Lunch.Website.ViewModels;
using StackExchange.Exceptional;
using Lunch.Core.Jobs;
using StructureMap;

namespace Lunch.Website.Controllers
{
    public class HomeController : BaseController
    {

        private readonly IRestaurantLogic _restaurantLogic;
        private readonly IRestaurantOptionLogic _restaurantOptionLogic;
        private readonly IVoteLogic _voteLogic;
        private readonly IUserLogic _userLogic;
        private readonly IVetoLogic _vetoLogic;
        private readonly IRestaurantRatingLogic _restaurantRatingLogic;

        public DateTime Overridetime = new DateTime(2013, 2, 15, 11, 00, 0);

        public HomeController(IRestaurantLogic restaurantLogic, IVoteLogic voteLogic, IRestaurantOptionLogic restaurantOptionLogic, IUserLogic userLogic, IVetoLogic vetoLogic, IRestaurantRatingLogic restaurantRatingLogic)
        {
            _restaurantLogic = restaurantLogic;
            _voteLogic = voteLogic;
            _restaurantOptionLogic = restaurantOptionLogic;
            _userLogic = userLogic;
            _vetoLogic = vetoLogic;
            _restaurantRatingLogic = restaurantRatingLogic;
        }

        public ActionResult DeleteVote(int id)
        {
            _voteLogic.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {

            //var _jobLogic = ObjectFactory.GetInstance<Jobs>();
            //_jobLogic.MorningMessage(null, 1);

            var model = new Homepage();
            if (Helpers.IsLunchDate(Core.Helpers.AdjustTimeOffsetFromUtc(DateTime.UtcNow)))
            {
                model.RestaurantsForToday = _restaurantOptionLogic.GetAndSaveOptions().ToList();
                model.YourVote = _voteLogic.GetItem(CurrentUser.Id, Core.Helpers.AdjustTimeOffsetFromUtc(DateTime.UtcNow));
                model.PeopleWhoVotedToday = _userLogic.GetListByVotedDate(null, null).ToList();
                model.WinningRestaurant = _restaurantOptionLogic.TodaysSelection();
                if (model.YourVote != null)
                {
                    model.YourVote.Restaurant = _restaurantLogic.Get(model.YourVote.RestaurantId);
                    if (model.WinningRestaurant != null)
                        model.YourRating = _restaurantRatingLogic.GetAllByUser(CurrentUser.Id).FirstOrDefault(f => f.RestaurantId == model.WinningRestaurant.RestaurantId);
                }

            }


            var currenttime = Lunch.Core.Helpers.AdjustTimeOffsetFromUtc(DateTime.UtcNow);

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

        public ActionResult SaveVeto(int id)
        {
            var veto = _vetoLogic.GetAllActiveForUser(CurrentUser.Id).First();
            veto.RestaurantId = id;
            veto.UsedAt = DateTime.UtcNow;
            veto.Used = true;
            _vetoLogic.SaveOrUpdate(veto);
            //message here? 
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
            if (!Helpers.IsLunchDate(Core.Helpers.AdjustTimeOffsetFromUtc(DateTime.UtcNow)))
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
                //if you vetoed today just show where we are now going
                var todayveto = _vetoLogic.GetUsedTodayForUser(CurrentUser.Id);
                if (todayveto != null)
                {
                    ViewBag.YouVetoed = true;
                    return View("going", model);
                }

                // If you have overrides you can override otherwise show results.
                var unusedvetos = _vetoLogic.GetAllActiveForUser(CurrentUser.Id);
                if (unusedvetos.Any() && model.YourVote != null)
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
