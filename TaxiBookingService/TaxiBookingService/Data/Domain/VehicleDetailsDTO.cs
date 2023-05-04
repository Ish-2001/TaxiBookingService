using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Domain
{
    public class VehicleDetailsDTO
    { 
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string RegisteredName { get; set; }

        [Required]
        [StringLength(4)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z0-9])*$")]
        public string ModelNumber { get; set; }

        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Enter a valid integer seating")]
        public int NumberOfSeats { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z0-9])*$")]
        public string RegisteredNumber { get; set; }


        [Required]
        [StringLength(10)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z0-9])*$")]
        public string Type { get; set; }

        [Required]
        [StringLength(50)]
        // [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string UserName { get; set; }
    }
}
