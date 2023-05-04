using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TaxiBookingService.Data.Domain
{
    public class DisplayUserDTO
    {
        
        public string FirstName { get; set; }

        
        public string LastName { get; set; }

        
        public string Gender { get; set; }

        
        public string Email { get; set; }

       
        public string PhoneNumber { get; set; }

    }
}
