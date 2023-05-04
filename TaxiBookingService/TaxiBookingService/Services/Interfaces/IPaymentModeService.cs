using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.Services.Interfaces
{
    public interface IPaymentModeService
    {
        List<PaymentMode> GetAll();
        List<DisplayPaymentModeDTO> GetPaymentModes();
        bool Add(PaymentModeDTO newMode);
        bool Update(PaymentModeDTO updatedMode, int id);
        bool Delete(int id);
    }
}
