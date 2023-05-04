using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Latitude { get; set; }

        [Required]
        public decimal Longitude { get; set; }

        [Required]
        [ForeignKey("State")]
        public int StateId { get; set; }

        [Required]
        [ForeignKey("City")]
        public int CityId { get; set; }

        [Required]
        [ForeignKey("Area")]
        public int AreaId { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [ForeignKey("User")]
        public int CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedAt { get; set; }

        [ForeignKey("Driver")]
        public int? ModifiedBy { get; set; }

        public virtual User User { get; set; }

        public virtual State State { get; set; }

        public virtual City City { get; set; }

        public virtual Area Area { get; set; }

        public virtual Driver Driver { get; set; }
    }
}
