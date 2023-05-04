using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Interfaces
{
    public interface IVehicleDetailsRepository : IGenericRepository<VehicleDetails>
    {
        List<VehicleDetails> GetAll();
    }

}
