using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunch.Core.Models.Views
{
    public class RestaurantImportSettings
    {
        [Required]
        [Display(Name = "Use Defaults?:")]
        public bool UseDefaults { get; set; }
        [Required]
        [Display(Name = "Publisher Key:")]
        public string PublisherKey { get; set; }
        [Required]
        [Display(Name = "Latitude:")]
        public string Latitude { get; set; }
        [Required]
        [Display(Name = "Longitude:")]
        public string Longitude { get; set; }
        [Required]
        [Display(Name = "Radius:")]
        public string Radius { get; set; }
    }
}
