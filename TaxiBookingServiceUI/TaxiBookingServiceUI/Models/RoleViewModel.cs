using System.ComponentModel.DataAnnotations;

namespace TaxiBookingServiceUI.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string Role { get; set; }

        public string UserName { get; set; }
    }
}
