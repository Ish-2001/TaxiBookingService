using Microsoft.EntityFrameworkCore;
using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Repositories
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(TaxiContext _context) : base(_context)
        {
        }

        public List<Location> GetAll()
        {
            return FindAll(item => item.IsDeleted == false)
                  .Include(item => item.City).Include(item => item.Area).Include(item => item.State).ToList();
        }
    }
}
