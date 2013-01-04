using System.Web.Mvc;
using AutoMapper;
using Lunch.Core.Logic;
using Lunch.Core.Models;
using Lunch.Website.Services;

namespace Lunch.Website.Controllers
{
    [LunchAuthorize(Roles = "Administrator")]
    public class RestaurantsController : BaseController
    {
        private readonly IRestaurantLogic _restaurantLogic;
        private readonly IRestaurantTypeLogic _restaurantTypeLogic;

        public RestaurantsController(IRestaurantLogic restaurantLogic, IRestaurantTypeLogic restaurantTypeLogic)
        {
            _restaurantLogic = restaurantLogic;
            _restaurantTypeLogic = restaurantTypeLogic;
        }


        public ActionResult Index(int? categoryid)
        {
            ViewBag.HasCategoryFilter = categoryid > 0;
            var result = _restaurantLogic.GetAllDetailed(categoryid);
            return View(result);
        }

        public ActionResult Details(int id)
        {
            var model = _restaurantLogic.Get(id);
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.RestaurantTypeList = _restaurantTypeLogic.GetAll();
            return View("Edit");
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] ViewModels.Restaurant model)
        {
            if (ModelState.IsValid)
            {
                var xmodel = Mapper.Map<ViewModels.Restaurant, Restaurant>(model);
                _restaurantLogic.SaveOrUpdate(xmodel);
                return RedirectToAction("Index");
            }
            ViewBag.RestaurantTypeList = _restaurantTypeLogic.GetAll();
            return View("Edit", model);
        }

        public ActionResult Edit(int id)
        {
            var rest = _restaurantLogic.Get(id);
            ViewBag.RestaurantTypeList = _restaurantTypeLogic.GetAll();
            var model = Mapper.Map<Restaurant, ViewModels.Restaurant>(rest);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.Restaurant model)
        {
            if (ModelState.IsValid)
            {
                var xmodel = Mapper.Map<ViewModels.Restaurant, Restaurant>(model);
                _restaurantLogic.SaveOrUpdate(xmodel);
                return RedirectToAction("Index");
            }
            ViewBag.RestaurantTypeList = _restaurantTypeLogic.GetAll();
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var rest = _restaurantLogic.Get(id);
            _restaurantLogic.Delete(rest);
            return RedirectToAction("Index");
        }
    }
}
