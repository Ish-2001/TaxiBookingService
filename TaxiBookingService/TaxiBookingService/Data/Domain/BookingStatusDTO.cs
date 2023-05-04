using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Domain
{
    public class BookingStatusDTO
    {
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string Status { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9._@ ]*$")]
        public string UserName { get; set; }
    }
}
