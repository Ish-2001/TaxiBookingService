using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Domain
{
    public class DisplayLocationDTO
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
    }
}
