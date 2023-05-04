using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Interfaces
{
    public interface IVehicleCategoryRepository : IGenericRepository<VehicleCategory>
    {
        public string GetCategoryName(int categoryId);
    }
}
