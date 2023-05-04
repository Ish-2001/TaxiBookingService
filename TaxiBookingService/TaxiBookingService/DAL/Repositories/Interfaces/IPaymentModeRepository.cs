using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Interfaces
{
    public interface IPaymentModeRepository : IGenericRepository<PaymentMode>
    {
        string GetModeName(int modeId);
    }
}
