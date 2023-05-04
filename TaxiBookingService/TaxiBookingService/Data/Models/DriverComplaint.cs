using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Models
{
    public class DriverComplaint
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(70)]
        [Column(TypeName = "varchar")]
        public string Complaint { get; set; }

        [ForeignKey("Driver")]
        public int DriverId { get; set; }

        [ForeignKey("Booking")]
        public int BookingId { get; set; }
        public int UserId { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        [Required]
        //[ForeignKey("User")]
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

        public virtual Driver Driver { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
