using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lunch.Website.ViewModels
{
    public class RestaurantType
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [DisplayName("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Category Name")]
        public string TypeName { get; set; }
    }
}