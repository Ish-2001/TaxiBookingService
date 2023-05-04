using System.ComponentModel.DataAnnotations;

namespace TaxiBookingServiceUI.Models
{
    public class CancellationReasonsViewModel
    {
        public int Id { get; set; }
        public string Reason { get; set; }

        public bool IsValid { get; set; }
    }
}
