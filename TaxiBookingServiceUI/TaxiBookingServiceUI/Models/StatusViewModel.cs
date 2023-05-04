using System.ComponentModel.DataAnnotations;

namespace TaxiBookingServiceUI.Models
{
    public class StatusViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string Status { get; set; }

      /*  [Required]
        [StringLength(50)]
        // [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string UserName { get; set; }*/
    }
}
