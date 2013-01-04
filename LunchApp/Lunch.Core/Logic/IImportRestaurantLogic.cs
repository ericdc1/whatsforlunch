using System.Collections.Generic;
using Lunch.Core.Models.Views;

namespace Lunch.Core.Logic
{
    public interface IImportRestaurantLogic
    {
        ImportRestaurant GetRestaurantsFromApi(string serviceBaseUrl, RestaurantImportSettings settings);
        IList<RestaurantToImport> CheckAlreadyImported(IList<RestaurantToImport> model);
    }
}
