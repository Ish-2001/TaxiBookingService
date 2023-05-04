using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Domain
{
    public class VehicleCategoryDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string Type { get; set; }

        [Required]
        [StringLength(50)]
        // [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string UserName { get; set; }
    }
}
