using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Domain
{
    public class StateDTO
    {
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        //[RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string UserName { get; set; }
    }
}
