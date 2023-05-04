using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        public List<Booking> GetAll();
        List<Booking> GetUserBookings(int userId);
        Booking GetLast(int userId, int driverId, int statusId);
    }
}
