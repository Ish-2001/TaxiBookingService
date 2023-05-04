using System.ComponentModel.DataAnnotations;

namespace TaxiBookingServiceUI.Models
{
    public class LocationViewModel
    {
        public int Id { get; set; }

        [Required]
        public decimal Latitude { get; set; }

        [Required]
        public decimal Longitude { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string State { get; set; }

       // public int StateId { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string City { get; set; }

        //public int CityId { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string Area { get; set; }

        //public int AreaId { get; set; }

        [Required]
        [StringLength(50)]
        //[RegularExpression("^[a-zA-Z]+@[a-zA-Z0-9]$", ErrorMessage = "Not a valid user name")]
        public string UserName { get; set; }
    }
}
