using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Lunch.Core.Logic;
using Lunch.Core.Models;
using Lunch.Core.Models.Views;
using Lunch.Website.Services;

namespace Lunch.Website.Controllers
{
    [LunchAuthorize(Roles = "Administrator")]
    public class RestaurantsController : BaseController
    {
        private readonly IRestaurantLogic _restaurantLogic;
        private readonly IRestaurantTypeLogic _restaurantTypeLogic;
        private readonly IRestaurantOptionLogic _restaurantOptionLogic;
        private readonly IImportRestaurantLogic _importRestaurantLogic;
        private const string SessionName = "WhatsForLunchRestaurantsImport";

        public RestaurantsController(IRestaurantLogic restaurantLogic, IRestaurantTypeLogic restaurantTypeLogic, IRestaurantOptionLogic restaurantOptionLogic, IImportRestaurantLogic importRestaurantLogic)
        {
            _restaurantLogic = restaurantLogic;
            _restaurantTypeLogic = restaurantTypeLogic;
            _restaurantOptionLogic = restaurantOptionLogic;
            _importRestaurantLogic = importRestaurantLogic;
        }

        //TODO: delete this?
        public ActionResult GetTop()
        {
            var temp = _restaurantLogic.GetSelection();

            //var results = _restaurantOptionLogic.GetAndSaveOptions();

            return View();
        }

        public ActionResult Index(int? categoryid)
        {
            ViewBag.HasCategoryFilter = categoryid > 0;
            var result = _restaurantLogic.GetAllDetailed(categoryid);
            return View(result.OrderBy(f => f.RestaurantName));
        }

        public ActionResult Create()
        {
            ViewBag.RestaurantTypeList = _restaurantTypeLogic.GetAll();
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] ViewModels.Restaurant model)
        {
            if (ModelState.IsValid)
            {
                if (model.RestaurantTypeID == null && !string.IsNullOrWhiteSpace(model.NewRestaurantTypeName))
                {
                    // Add new restaurant type
                    var newType = new RestaurantType {TypeName = model.NewRestaurantTypeName.Trim()};
                    newType = _restaurantTypeLogic.SaveOrUpdate(newType);
                    if (newType != null)
                        model.RestaurantTypeID = newType.Id;
                }
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
                if (model.RestaurantTypeID == null && !string.IsNullOrWhiteSpace(model.NewRestaurantTypeName))
                {
                    // Add new restaurant type
                    var newType = new RestaurantType { TypeName = model.NewRestaurantTypeName.Trim() };
                    newType = _restaurantTypeLogic.SaveOrUpdate(newType);
                    if (newType != null)
                        model.RestaurantTypeID = newType.Id;
                }
                var xmodel = Mapper.Map<ViewModels.Restaurant, Restaurant>(model);
                _restaurantLogic.SaveOrUpdate(xmodel);
                return RedirectToAction("Index");
            }
            ViewBag.RestaurantTypeList = _restaurantTypeLogic.GetAll();
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var model = _restaurantLogic.Get(id);
            if (model != null)
                _restaurantLogic.Delete(model);
            return RedirectToAction("Index");
        }

        #region Restaurant Types

        public ActionResult Types()
        {
            return View(_restaurantTypeLogic.GetAll());
        }

        public ActionResult CreateCategory()
        {
            return View(new ViewModels.RestaurantType());
        }

        [HttpPost]
        public ActionResult CreateCategory(ViewModels.RestaurantType model)
        {
            if (ModelState.IsValid)
            {
                _restaurantTypeLogic.SaveOrUpdate(Mapper.Map<ViewModels.RestaurantType, RestaurantType>(model));
                return RedirectToAction("Types");
            }
            return View(model);
        }

        public ActionResult EditCategory(int id)
        {
            var entity = _restaurantTypeLogic.Get(id);
            return View(Mapper.Map<RestaurantType, ViewModels.RestaurantType>(entity));
        }

        [HttpPost]
        public ActionResult EditCategory(ViewModels.RestaurantType model)
        {
            if (ModelState.IsValid)
            {
                _restaurantTypeLogic.SaveOrUpdate(Mapper.Map<ViewModels.RestaurantType, RestaurantType>(model));
                return RedirectToAction("Types");
            }
            return View(model);
        }

        public ActionResult DeleteCategory(int id)
        {
            var model = _restaurantTypeLogic.Get(id);
            if (model != null)
                _restaurantTypeLogic.Delete(model);
            return RedirectToAction("Types");
        }

        #endregion

        #region Import Restaurants

        public ActionResult ImportSettings()
        {
            var defaults = GetDefaultImportSetting();
            var model = new RestaurantImportSettings
            {
                UseDefaults = true,
                PublisherKey = defaults.PublisherKey,
                Latitude = defaults.Latitude,
                Longitude = defaults.Longitude,
                Radius = defaults.Radius
            };
            ViewBag.DefaultSettings = defaults;
            return View(model);
        }

        [HttpPost]
        public ActionResult ImportSettings(RestaurantImportSettings model)
        {
            if (ModelState.IsValid)
            {
                var results = _importRestaurantLogic.GetRestaurantsFromApi(ConfigurationManager.AppSettings.Get("RestaurantProviderURL"), model);
                Session.Add(SessionName, results);
                GenerateDropDownLists();
                return View("DisplayRestaurants", results);
            }
            return View(model);
        }

        public PartialViewResult Search(string term)
        {
            ImportRestaurant results;
            if (Session[SessionName] == null)
            {
                results = _importRestaurantLogic.GetRestaurantsFromApi(ConfigurationManager.AppSettings.Get("RestaurantProviderURL"), GetDefaultImportSetting());
            }
            else
            {
                results = (ImportRestaurant)Session[SessionName];
                results.Restaurants = _importRestaurantLogic.CheckAlreadyImported(results.Restaurants);
            }
            var model = new ImportRestaurant { TotalResults = results.TotalResults, Page = results.Page, FirstHit = results.FirstHit, LastHit = results.LastHit, Restaurants = results.Restaurants };
            if (!string.IsNullOrWhiteSpace(term) && term.Length > 1)
                model.Restaurants = model.Restaurants.Where(f => f.RestaurantName.ToLower().Contains(term.ToLower())).ToList();
            GenerateDropDownLists();
            return PartialView("_ImportRestaurantsList", model);
        }

        [HttpPost]
        public PartialViewResult SaveImports(ImportRestaurant model)
        {
            if (model != null && model.Restaurants.Any())
            {
                var restaurantsToImport = new List<Restaurant>();
                model.Restaurants = _importRestaurantLogic.CheckAlreadyImported(model.Restaurants);
                foreach (var restaurant in model.Restaurants)
                {
                    if (!restaurant.AlreadyImported)
                        restaurantsToImport.Add(restaurant.ToDomainModel());
                }
                _restaurantLogic.SaveOrUpdateAll(restaurantsToImport.ToArray());
                model.Restaurants = _importRestaurantLogic.CheckAlreadyImported(model.Restaurants);
            }
            GenerateDropDownLists();
            return PartialView("~/Views/Restaurants/_ImportRestaurantsList.cshtml", model);
        }

        #region Helpers

        private static RestaurantImportSettings GetDefaultImportSetting()
        {
            var defaultSettings = new RestaurantImportSettings
            {
                PublisherKey = ConfigurationManager.AppSettings["RestaurantProviderPublisherKey"],
                Latitude = ConfigurationManager.AppSettings["RestaurantProviderLatitude"],
                Longitude = ConfigurationManager.AppSettings["RestaurantProviderLongitude"],
                Radius = ConfigurationManager.AppSettings["RestaurantProviderRadius"]
            };
            return defaultSettings;
        }

        private void GenerateDropDownLists()
        {
            // Specials day.
            var values = from DayOfWeek e in Enum.GetValues(typeof(DayOfWeek)) select new { Id = (int)e, Name = e.ToString() };
            ViewBag.DaysOfWeek = new SelectList(values, "Id", "Name");
            // Restaurant type.
            ViewBag.RestaurantTypes = new SelectList(_restaurantTypeLogic.GetAll().OrderBy(f => f.TypeName), "Id", "TypeName");
        }

        #endregion

        #endregion
    }
}
