using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TaxiBookingService.Data.Domain
{
    public class DisplayDriverDTO
    {
        public string LicenseNumber { get; set; }      
        public string FirstName { get; set; }      
        public string LastName { get; set; }      
        public string Gender { get; set; }      
        public string Email { get; set; }  
        public string PhoneNumber { get; set; }
        public double Rating { get; set; }
    }
}
