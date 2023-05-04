using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Domain
{
    public class RideCancellationDTO
    {
        public int Id { get; set; }

        //[Required]
        public double CancellationFee { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string Reason { get; set; }

        public int BookingId { get; set; }

        [Required]
        public bool IsPending { get; set; }
    }
}
