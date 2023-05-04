using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Domain
{
    public class BookingDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
       // [RegularExpression(@"^[A-za-z,]*((-|\s)*[A-Za-z,])*$")]
        public string PickupLocation { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(100)]     
        public string Destination { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9._@ ]*$")]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string Status { get; set; }

        [Required]
        public decimal RideFare { get; set; }

        [Required]
        public decimal RideTime { get; set; }

        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string PaymentMode { get; set; }

        public bool IsActive { get; set; }
    }
}
