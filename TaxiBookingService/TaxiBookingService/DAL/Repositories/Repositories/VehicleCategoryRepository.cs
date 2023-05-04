using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Repositories
{
    public class VehicleCategoryRepository : GenericRepository<VehicleCategory>, IVehicleCategoryRepository
    {
        public VehicleCategoryRepository(TaxiContext _context) : base(_context)
        {
        }

        public string GetCategoryName(int categoryId)
        {
            return FindAll(item => item.Id == categoryId).Select(item => item.Type).FirstOrDefault();
        }
    }
}
