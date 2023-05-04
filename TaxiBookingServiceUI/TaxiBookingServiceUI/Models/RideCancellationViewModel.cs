using System.ComponentModel.DataAnnotations;

namespace TaxiBookingServiceUI.Models
{
    public class RideCancellationViewModel
    {
        public int Id { get; set; }

        public double CancellationFee { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string Reason { get; set; }

        public int ReasonId { get; set; }

        public int BookingId { get; set; }

        public bool IsPending { get; set; }
    }
}
