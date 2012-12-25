using System.Configuration;
using System.IO;
using System.Net;
using System.Web.Mvc;
using Lunch.Core.Helpers;
using Lunch.Website.ViewModels;

namespace Lunch.Website.Controllers
{
    public class ManagementController : Controller
    {
        //
        // GET: /Management/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RestaurantImport()
        {
            var defaults = GetDefaultImportSetting();
            var model = new ImportSettingsViewModel
                            {
                                UseDefaults = true, PublisherKey = defaults.PublisherKey, Latitude = defaults.Latitude, Longitude = defaults.Longitude, Radius = defaults.Radius
                            };
            ViewBag.DefaultSettings = defaults;
            return View(model);
        }

        [HttpPost]
        public ActionResult RestaurantImport(ImportSettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var results = GetRestaurantsFromApi(model);
                return View("DisplayRestaurants", results);
            }
            return View(model);
        }

        public ImportRestaurantsViewModel GetRestaurantsFromApi(ImportSettingsViewModel settings)
        {
            var page = 1;
            var results = new ImportRestaurantsViewModel {TotalResults = 51};
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
                        results.Restaurants.Add(new RestaurantViewModel {Name = item.name, Selected = false});
                    }
                }
                page++;
            }
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
    }
}
