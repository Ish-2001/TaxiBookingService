using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Domain
{
    public class LoginDTO
    {
        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z0-9._@ ]+$")]
        public string UserName { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^((?=.*@)(?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Password should contain atleast 1 capital letter,atleast 1 small letter and special character @")]
        public string Password { get; set; }
    }
}
