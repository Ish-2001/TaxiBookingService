using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Interfaces
{
    public interface ICityRepository : IGenericRepository<City>
    {
        List<City> GetAll();
    }
}
