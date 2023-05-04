using Microsoft.EntityFrameworkCore;
using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(TaxiContext _context) : base(_context)
        {
        }

        public List<Booking> GetAll()
        {
            return FindAll(item => item.IsDeleted == false).Include(item => item.Status).Include(item => item.User)
                  .Include(item => item.PaymentMode).ToList();
        }

        public List<Booking> GetUserBookings(int userId)
        {
            return FindAll(item => item.IsDeleted == false && item.UserId == userId).Include(item => item.Status).Include(item => item.User)
                  .Include(item => item.PaymentMode).ToList();
        }

        public Booking GetLast(int userId, int driverId , int statusId)
        {
            return FindAll(item => item.UserId == userId && item.DriverId == driverId && item.StatusId == statusId)
                  .OrderBy(item => item.Id).LastOrDefault();
        }
    }
}
