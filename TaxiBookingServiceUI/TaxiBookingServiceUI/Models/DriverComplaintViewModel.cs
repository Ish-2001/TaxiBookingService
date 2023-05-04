using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaxiBookingServiceUI.Models
{
    public class DriverComplaintViewModel
    {
        [Required]
        [StringLength(70)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string Complaint { get; set; }
        public string UserName { get; set; }
        public string DriverName { get; set; }

        [DisplayName("User Name")]
        public string NameUser { get; set; }

        [DisplayName("Driver Name")]
        public string NameDriver { get; set; }

        public int DriverId { get; set; }
        public int UserId { get; set; }
    }
}
