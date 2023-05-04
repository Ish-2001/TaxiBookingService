using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Domain
{
    public class DisplayAreaDTO
    {
        public string Name { get; set; }
        public string Locality { get; set; }
        public string City { get; set; }
    }
}
