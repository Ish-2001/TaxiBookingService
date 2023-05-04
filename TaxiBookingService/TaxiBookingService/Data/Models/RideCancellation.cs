using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiBookingService.Data.Models
{
    public class RideCancellation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double CancellationFee { get; set; }

        [Required]
        [ForeignKey("Booking")]
        public int BookingId { get; set; }

        [Required]
        [ForeignKey("CancellationReason")]
        public int ReasonId { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsPending { get; set; }

        [Required]
        //[ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedAt { get; set; }

        //[ForeignKey("User")]
        public int? ModifiedBy { get; set; }

        [ForeignKey(nameof(ModifiedBy))]
        public virtual User Modified { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public virtual User Created { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public virtual Booking Booking { get; set; }
        
        public virtual CancellationReason CancellationReason { get; set; }
    }
}
