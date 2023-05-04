using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Domain
{
    public class DisplayVehicleDetailsDTO
    {        
        public string Name { get; set; }
        public string RegisteredName { get; set; }
        public string ModelNumber { get; set; }
        public int NumberOfSeats { get; set; }
        public string RegisteredNumber { get; set; }
        public string Type { get; set; }
    }
}
