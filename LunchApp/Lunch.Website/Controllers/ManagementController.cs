using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Lunch.Core.Logic;
using Lunch.Core.Models;
using Lunch.Core.Models.Views;

namespace Lunch.Website.Controllers
{
    public class ManagementController : Controller
    {        
        #region "Fields"

        private readonly IRestaurantTypeLogic _restaurantTypeLogic;
        private readonly IRestaurantLogic _restaurantLogic;
        private readonly IImportRestaurantLogic _importRestaurantLogic;
        private const string SessionName = "WhatsForLunchRestaurantsImport";

        #endregion

        #region "Constructors"

        public ManagementController(IRestaurantLogic restaurantLogic, IRestaurantTypeLogic restaurantTypeLogic, IImportRestaurantLogic importRestaurantLogic)
        {
            _restaurantTypeLogic = restaurantTypeLogic;
            _restaurantLogic = restaurantLogic;
            _importRestaurantLogic = importRestaurantLogic;
        }

        #endregion

        public ActionResult Index()
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
        public ActionResult Index(RestaurantImportSettings model)
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
            return PartialView("~/Views/Management/_ImportRestaurantsList.cshtml", model);
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
    }
}
