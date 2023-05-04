using Microsoft.EntityFrameworkCore;
using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(TaxiContext _context) : base(_context)
        {

        }

        public List<City> GetAll()
        {
            return FindAll(item => item.IsDeleted == false).Include(item => item.State).ToList();
        }
    }
}
