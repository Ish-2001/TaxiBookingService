using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Interfaces
{
    public interface IBookingStatusRepository : IGenericRepository<BookingStatus>
    {
        string GetStatusName(int statusId);
    }
}
