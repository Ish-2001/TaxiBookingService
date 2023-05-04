using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Models
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string LicenseNumber { get; set; }

        [Required]
        public double Rating { get; set; }

        [ForeignKey("Location")]
        public int? LocationId { get; set; }


        [ForeignKey("VehicleCategory")]
        public int? VehicleCategoryId { get; set; }

       
        [ForeignKey("VehicleDetails")]
        public int? VehicleDetailId { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsApproved { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }

        [Required]
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

        public int? ModifiedBy { get; set; }

        [ForeignKey(nameof(ModifiedBy))]
        public virtual User Modified { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public virtual User Created { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public virtual VehicleCategory VehicleCategory { get; set; }

        public virtual VehicleDetails VehicleDetails { get; set; }

        public virtual Location Location { get; set; }

    }
}
