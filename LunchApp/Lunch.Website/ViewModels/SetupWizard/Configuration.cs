using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lunch.Website.ViewModels.SetupWizard
{
    public class Configuration
    {   
        [Required]
        public string AzureSQL { get; set; }
        [Required]
        public string BaseURL { get; set; }
        [Required]
        public string FromEmail { get; set; }
        
        public string ExcludedDays { get; set; }
        
        
        public string RestaurantProviderPublisherKey { get; set; }
        public string RestaurantProviderLatitude { get; set; }
        public string RestaurantProviderLongitude { get; set; }

        public int? RestaurantProviderRadius { get; set; }

        [MaxLength(5)]
        public int? RestaurantProviderZipCode { get; set; }
    }
}