using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.Services.Interfaces
{
    public interface IVehicleDetailsService
    {
        List<VehicleDetails> GetAll();
        List<DisplayVehicleDetailsDTO> GetVehicleDetails();
        bool Add(VehicleDetailsDTO newDetail);
        bool Update(VehicleDetailsDTO updatedDetail, int id);
        bool Delete(int id);
    }
}
