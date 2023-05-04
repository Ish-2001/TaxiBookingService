using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Domain
{
    public class DisplayBookingDTO
    { 
        public string PickupLocation { get; set; }
        public DateTime Date { get; set; }
        public string Destination { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public decimal RideFare { get; set; }
        public decimal RideTime { get; set; }
        public string PaymentMode { get; set; }
    }
}
