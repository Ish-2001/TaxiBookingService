using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Domain
{
    public class CityDTO
    {
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string State { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9._@ ]*$")]
        public string UserName { get; set; }
    }
}
