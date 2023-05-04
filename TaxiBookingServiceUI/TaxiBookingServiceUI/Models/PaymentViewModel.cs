using System.ComponentModel.DataAnnotations;

namespace TaxiBookingServiceUI.Models
{
    public class PaymentViewModel
    {
        public int Id { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]

        public DateTime Date { get; set; }

        [Required]

        public int BookingId { get; set; }

        [Required]

        public int ModeId { get; set; }
    }
}
