using System.ComponentModel.DataAnnotations;

namespace TaxiBookingServiceUI.Models
{
    public class CityViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string Name { get; set; }

        //[Required]
        public string State { get; set; }

        public int StateId { get; set; }

        public string UserName { get; set; }
    }
}
