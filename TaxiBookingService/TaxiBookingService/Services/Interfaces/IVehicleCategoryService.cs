using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.Services.Interfaces
{
    public interface IVehicleCategoryService
    {
        List<VehicleCategory> GetAll();
        List<DisplayVehicleCategoryDTO> GetVehicleCategories();
        bool Add(VehicleCategoryDTO newCategory);
        bool Update(VehicleCategoryDTO updatedCategory, int id);
        bool Delete(int id);
    }
}
