using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Repositories
{
    public class BookingStatusRepository : GenericRepository<BookingStatus>, IBookingStatusRepository
    {
        public BookingStatusRepository(TaxiContext _context) : base(_context)
        {
        }

        public string GetStatusName(int statusId)
        {
            return FindAll(item => item.Id == statusId).Select(item => item.Status).FirstOrDefault();
        }
    }
}
