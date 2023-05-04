using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.Services.Interfaces
{
    public interface IAreaService
    {
        List<Area> GetAll();
        List<DisplayAreaDTO> GetAreas();
        bool Add(AreaDTO newArea);
        bool Update(AreaDTO updatedArea, int id);
        bool Delete(int id);
    }
}
