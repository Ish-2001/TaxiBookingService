using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NodaTime;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar")]
        public string PickupLocation { get; set; }

        [Required]
        [Column(TypeName = "datetime2(7)")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar")]
        public string Destination { get; set; }

        [Required]
        public decimal RideFare { get; set; }

        [Required]
        public decimal RideTime { get; set; }

        /*[Required]

        public int LocationId { get; set; }*/

        [Required]
        [ForeignKey("Status")]
        public int StatusId { get; set; }

        //[Required]
        [ForeignKey("Rating")]
        public int? RatingId { get; set; }

        //[Required]
        [ForeignKey("Driver")]
        public int? DriverId { get; set; }

        [Required]
        //[ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("PaymentMode")]
        public int? ModeId { get; set; }

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

        public virtual DriverRating Rating { get; set; }

        public virtual BookingStatus Status { get; set; }

        public virtual PaymentMode PaymentMode { get; set; }

    }
}
