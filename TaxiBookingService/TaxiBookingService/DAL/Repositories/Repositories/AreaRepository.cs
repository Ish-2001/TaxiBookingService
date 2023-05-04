using Microsoft.EntityFrameworkCore;
using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Repositories
{
    public class AreaRepository : GenericRepository<Area>, IAreaRepository
    {
        public AreaRepository(TaxiContext _context) : base(_context)
        {

        }

        public List<Area> GetAll()
        {
            return FindAll(item => item.IsDeleted == false).Include(item => item.City).ToList();
        }
    }
}
