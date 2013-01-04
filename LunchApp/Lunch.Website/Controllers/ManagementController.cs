using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Lunch.Core.Jobs;
using Lunch.Website.Services;
using Lunch.Website.ViewModels;

namespace Lunch.Website.Controllers
{
    [LunchAuthorize(Roles = "Administrator")]
    public class ManagementController : BaseController
    {
        private const string SessionName = "WhatsForLunchRestaurantsImport";

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
                model.Restaurants = model.Restaurants.Where(f => f.Name.ToLower().Contains(term.ToLower())).ToList();
            GenerateDropDownLists();
            return PartialView("_ImportRestaurantsList", model);
        }

        [HttpPost]
        public ActionResult SaveImports(ImportRestaurantsViewModel model)
        {
            return View(model);
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
                    if (results.Restaurants.Find(f => f.Name == item.name) == null)
                    {
                        results.Restaurants.Add(new RestaurantViewModel { Name = item.name, Selected = false, SpecialDay = null });
                    }
                }
                page++;
            }
            results.Restaurants = results.Restaurants.OrderBy(f => f.Name).ToList();
            return results;
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
            var values = from DayOfWeek e in Enum.GetValues(typeof(DayOfWeek)) select new { Id = e, Name = e.ToString() };
            ViewBag.DaysOfWeek = new SelectList(values, "Id", "Name");
        }

        #endregion
    }
}
