using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Lunch.Core.Models.Views;

namespace Lunch.Core.Logic.Implementations
{
    public class ImportRestaurantLogic : IImportRestaurantLogic
    {
        #region "Fields"

        private readonly IRestaurantLogic _restaurantLogic;

        #endregion

        #region "Constructors"

        public ImportRestaurantLogic(IRestaurantLogic restaurantLogic)
        {
            _restaurantLogic = restaurantLogic;
        }

        #endregion

        public ImportRestaurant GetRestaurantsFromApi(string serviceBaseUrl, RestaurantImportSettings settings)
        {
            var page = 1;
            var results = new ImportRestaurant { TotalResults = 51 };
            while (results.TotalResults > page * 50)
            {
                var apiUrl = GenerateRestaurantApiUrl(serviceBaseUrl, settings.PublisherKey, settings.Latitude, settings.Longitude, settings.Radius, page);
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
                        results.Restaurants.Add(new RestaurantToImport { RestaurantName = item.name, Selected = false, PreferredDayOfWeek = null });
                    }
                }
                page++;
            }
            results.Restaurants = results.Restaurants.OrderBy(f => f.RestaurantName).ToList();
            results.Restaurants = CheckAlreadyImported(results.Restaurants);
            return results;
        }

        public IList<RestaurantToImport> CheckAlreadyImported(IList<RestaurantToImport> model)
        {
            if (model.Any())
            {
                var inDBAlready = _restaurantLogic.GetList(new { }).ToList();
                if (inDBAlready.Any())
                {
                    foreach (var item in model.Where(item => inDBAlready.Find(f => f.RestaurantName.Trim().ToLower() == item.RestaurantName.Trim().ToLower()) != null))
                    {
                        item.AlreadyImported = true;
                    }
                }
            }
            return model;
        }

        public static string GenerateRestaurantApiUrl(string serviceBaseUrl, string publisher, string latitude, string longitude, string radius, int page)
        {
            var apiUrl = string.Empty;
            if (!string.IsNullOrWhiteSpace(serviceBaseUrl) && !string.IsNullOrWhiteSpace(publisher) && !string.IsNullOrWhiteSpace(latitude) && !string.IsNullOrWhiteSpace(longitude) && !string.IsNullOrWhiteSpace(radius))
            {
                var sb = new StringBuilder();
                sb.Append(serviceBaseUrl);
                sb.Append(@"?type=restaurant&rpp=50&format=json");
                sb.Append(@"&publisher=" + publisher);
                sb.Append(@"&lat=" + latitude);
                sb.Append(@"&lon=" + longitude);
                sb.Append(@"&radius=" + radius);
                sb.Append(@"&page=" + page.ToString());
                apiUrl = sb.ToString();
            }
            return apiUrl;
        }
    }
}
