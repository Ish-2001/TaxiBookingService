using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Repositories
{
    public class DriverRatingRepository : GenericRepository<DriverRating>, IDriverRatingRepository
    {
        public DriverRatingRepository(TaxiContext _context) : base(_context)
        {

        }
    }
}
