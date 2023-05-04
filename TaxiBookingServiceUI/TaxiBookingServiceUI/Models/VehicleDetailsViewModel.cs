using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaxiBookingServiceUI.Models
{
    public class VehicleDetailsViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        [DisplayName("Registered Name")]
        public string RegisteredName { get; set; }

        [Required]
        [StringLength(4)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z0-9])*$")]
        [DisplayName("Model Number")]
        public string ModelNumber { get; set; }

        [Required]
        [Range(4, int.MaxValue, ErrorMessage = "Enter a valid integer seating")]
        [DisplayName("Number of Seats")]
        public int NumberOfSeats { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z0-9])*$")]
        [DisplayName("Registered Number")]
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
