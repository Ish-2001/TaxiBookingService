using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Interfaces
{
    public interface ILocationRepository : IGenericRepository<Location>
    {
        List<Location> GetAll();
    }
}
