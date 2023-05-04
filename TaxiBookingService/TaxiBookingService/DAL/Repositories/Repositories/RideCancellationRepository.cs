using Microsoft.EntityFrameworkCore;
using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Repositories
{
    public class RideCancellationRepository : GenericRepository<RideCancellation>, IRideCancellationRepository
    {
        public RideCancellationRepository(TaxiContext _context) : base(_context)
        {
        }

        public List<RideCancellation> GetAll()
        {
            return FindAll(item => item.IsDeleted == false).Include(item => item.User).Include(item => item.CancellationReason).ToList();
        }
    }
}
