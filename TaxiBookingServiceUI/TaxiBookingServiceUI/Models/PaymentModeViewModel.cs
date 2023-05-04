using System.ComponentModel.DataAnnotations;

namespace TaxiBookingServiceUI.Models
{
    public class PaymentModeViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string Mode { get; set; }

        [Required]
        [StringLength(50)]
        //[RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string UserName { get; set; }
    }
}
