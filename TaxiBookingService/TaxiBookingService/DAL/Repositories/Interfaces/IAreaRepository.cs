using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Interfaces
{
    public interface IAreaRepository : IGenericRepository<Area>
    {
        List<Area> GetAll();
    }
}
