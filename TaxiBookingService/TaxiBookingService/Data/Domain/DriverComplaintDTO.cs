using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiBookingService.Data.Domain
{
    public class DriverComplaintDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(70)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string Complaint { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9._@ ]*$")]
        public string UserName { get; set; }

        public string DriverName { get; set; }
    }
}
