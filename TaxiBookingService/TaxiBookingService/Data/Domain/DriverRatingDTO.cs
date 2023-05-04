using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Domain
{
    public class DriverRatingDTO
    {
        public int Id { get; set; }

        [Required]
        public double Rating { get; set; }

        [Required]
        public int BookingId { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
