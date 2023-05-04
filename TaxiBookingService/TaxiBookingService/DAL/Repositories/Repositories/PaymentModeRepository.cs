using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Repositories
{
    public class PaymentModeRepository : GenericRepository<PaymentMode>, IPaymentModeRepository
    {
        public PaymentModeRepository(TaxiContext _context) : base(_context)
        {
        }

        public string GetModeName(int modeId)
        {
            return FindAll(item => item.Id == modeId).Select(item => item.Mode).FirstOrDefault();
        }
    }
}
