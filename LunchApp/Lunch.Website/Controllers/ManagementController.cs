using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Lunch.Core.Jobs;
using Lunch.Core.Logic;
using Lunch.Website.ViewModels;
using Restaurant = Lunch.Core.Models.Restaurant;

namespace Lunch.Website.Controllers
{
    public class ManagementController : Controller
    {        
        #region "Fields"

        private readonly IRestaurantTypeLogic _restaurantTypeLogic;
        private IRestaurantLogic _restaurantLogic;
        private const string SessionName = "WhatsForLunchRestaurantsImport";

        #endregion

        #region "Constructors"

        public ManagementController(IRestaurantLogic restaurantLogic, IRestaurantTypeLogic restaurantTypeLogic)
        {
            _restaurantTypeLogic = restaurantTypeLogic;
            _restaurantLogic = restaurantLogic;
        }

        #endregion

        public ActionResult Index()
        {
            var defaults = GetDefaultImportSetting();
            var model = new ImportSettingsViewModel
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
        public ActionResult Index(ImportSettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var results = GetRestaurantsFromApi(model);
                Session.Add(SessionName, results);
                GenerateDropDownLists();
                return View("DisplayRestaurants", results);
            }
            return View(model);
        }

        public PartialViewResult Search(string term)
        {
            ImportRestaurantsViewModel results;
            if (Session[SessionName] == null)
            {
                results = GetRestaurantsFromApi(GetDefaultImportSetting());
            }
            else
            {
                results = (ImportRestaurantsViewModel)Session[SessionName];
            }
            var model = new ImportRestaurantsViewModel { TotalResults = results.TotalResults, Page = results.Page, FirstHit = results.FirstHit, LastHit = results.LastHit, Restaurants = results.Restaurants };
            if (!string.IsNullOrWhiteSpace(term) && term.Length > 1)
                model.Restaurants = model.Restaurants.Where(f => f.RestaurantName.ToLower().Contains(term.ToLower())).ToList();
            model.Restaurants = CheckAlreadyImported(model);
            GenerateDropDownLists();
            return PartialView("_ImportRestaurantsList", model);
        }

        [HttpPost]
        public PartialViewResult SaveImports(ImportRestaurantsViewModel model)
        {
            if (model != null && model.Restaurants.Any())
            {
                var restaurantsToImport = new List<Restaurant>();
                model.Restaurants = CheckAlreadyImported(model);
                foreach (var restaurant in model.Restaurants)
                {
                    if (!restaurant.AlreadyImported)
                    {
                        var toAdd = restaurant.ToDomainModel();
                        restaurantsToImport.Add(toAdd);
                    }
                }
                _restaurantLogic.SaveOrUpdateAll(restaurantsToImport.ToArray());
                model.Restaurants = CheckAlreadyImported(model);
            }
            GenerateDropDownLists();
            return PartialView("~/Views/Management/_ImportRestaurantsList.cshtml", model);
        }

        #region Helpers

        private ImportRestaurantsViewModel GetRestaurantsFromApi(ImportSettingsViewModel settings)
        {
            var page = 1;
            var results = new ImportRestaurantsViewModel { TotalResults = 51 };
            while (results.TotalResults > page * 50)
            {
                var apiUrl = Helpers.GenerateRestaurantApi(settings.PublisherKey, settings.Latitude, settings.Longitude, settings.Radius, page);
                var objRequest = WebRequest.Create(apiUrl);
                var objResponse = objRequest.GetResponse();
                string strResult;
                using (var sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    strResult = sr.ReadToEnd();
                }
                var data = System.Web.Helpers.Json.Decode(strResult);
                results.TotalResults = data.results.total_hits;
                results.FirstHit = data.results.first_hit;
                results.LastHit = data.results.last_hit;
                results.Page = data.results.page;
                foreach (var item in data.results.locations)
                {
                    if (results.Restaurants.ToList().Find(f => f.RestaurantName == item.name) == null)
                    {
                        results.Restaurants.Add(new RestaurantViewModel { RestaurantName = item.name, Selected = false, PreferredDayOfWeek = null });
                    }
                }
                page++;
            }
            results.Restaurants = results.Restaurants.OrderBy(f => f.RestaurantName).ToList();
            results.Restaurants = CheckAlreadyImported(results);
            return results;
        }

        private IList<RestaurantViewModel> CheckAlreadyImported(ImportRestaurantsViewModel model)
        {
            if (model.Restaurants.Any())
            {
                var inDBAlready = _restaurantLogic.GetList(new { });
                if (inDBAlready.Any())
                {
                    foreach (var item in model.Restaurants)
                    {
                        if (inDBAlready.ToList().Find(f => f.RestaurantName.Trim().ToLower() == item.RestaurantName.Trim().ToLower()) != null)
                        {
                            item.AlreadyImported = true;
                        }
                    }
                }
            }
            return model.Restaurants;
        }

        private static ImportSettingsViewModel GetDefaultImportSetting()
        {
            var defaultSettings = new ImportSettingsViewModel
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
            var values = from DayOfWeek e in Enum.GetValues(typeof(DayOfWeek)) select new { Id = e, Name = e.ToString() };
            ViewBag.DaysOfWeek = new SelectList(values, "Id", "Name");
            // Restaurant type.
            ViewBag.RestaurantTypes = new SelectList(_restaurantTypeLogic.GetAll().OrderBy(f => f.TypeName), "Id", "TypeName");
        }

        #endregion
    }
}
