using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Interfaces
{
    public interface IRideCancellationRepository : IGenericRepository<RideCancellation>
    {
        List<RideCancellation> GetAll();
    }
}
