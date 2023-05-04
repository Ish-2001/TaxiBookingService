using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.Services.Interfaces
{
    public interface ILocationService
    {
        List<Location> GetAll();
        List<DisplayLocationDTO> GetLocations();
        bool Add(LocationDTO newLocation);
        bool Update(LocationDTO updatedLocation, int id);
        bool Delete(int id);
    }
}
