using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.Services.Interfaces
{
    public interface IRideCancellationService
    {
        bool Add(RideCancellationDTO rideCancellationDTO);
        List<RideCancellation> GetAll();
        List<DisplayRideCancellationDTO> GetPaymentModes();
        public bool PayCancellationFee(string userName);
    }
}
