using Microsoft.EntityFrameworkCore;
using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Repositories
{
    public class VehicleDetailsRepository : GenericRepository<VehicleDetails>, IVehicleDetailsRepository
    {
        public VehicleDetailsRepository(TaxiContext _context) : base(_context)
        {
        }

        public List<VehicleDetails> GetAll()
        {
            return FindAll(item => item.IsDeleted == false).Include(item => item.Vehicle).ToList();
        }

    }
}
