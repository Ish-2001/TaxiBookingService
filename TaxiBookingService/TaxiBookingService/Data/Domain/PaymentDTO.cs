using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Domain
{
    public class PaymentDTO
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
