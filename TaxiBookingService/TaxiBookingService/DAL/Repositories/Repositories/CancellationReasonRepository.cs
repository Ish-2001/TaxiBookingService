using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Repositories
{
    public class CancellationReasonRepository : GenericRepository<CancellationReason>, ICancellationReasonRepository
    {
        public CancellationReasonRepository(TaxiContext _context) : base(_context)
        {
        }
    }
}
