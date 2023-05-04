using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Models
{
    public class VehicleDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string RegisteredName { get; set; }


        [Required]
        [StringLength(4)]
        [Column(TypeName = "varchar")]
        public string ModelNumber { get; set; }

        [Required]
        public int NumberOfSeats { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar")]
        public string RegisteredNumber { get; set; }

        [Required]
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

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

        public virtual VehicleCategory Vehicle { get; set; }
    }
}
