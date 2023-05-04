using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaxiBookingServiceUI.Models
{
    public class VehicleCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        [DisplayName("Vehicle Type")]
        public string Type { get; set; }

        [Required]
        [StringLength(50)]
        // [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string UserName { get; set; }
    }
}
