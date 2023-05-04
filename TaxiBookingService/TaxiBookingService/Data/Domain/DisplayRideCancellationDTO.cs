using System.ComponentModel.DataAnnotations;

namespace TaxiBookingService.Data.Domain
{
    public class DisplayRideCancellationDTO
    {
        public double CancellationFee { get; set; }
        public string UserName { get; set; }
        public string Reason { get; set; }
    }
}
