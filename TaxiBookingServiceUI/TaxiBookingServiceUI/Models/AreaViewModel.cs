using System.ComponentModel.DataAnnotations;

namespace TaxiBookingServiceUI.Models
{
    public class AreaViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string Locality { get; set; }

        public int CityId { get; set; }

        public string UserName { get; set; }
    }
}
