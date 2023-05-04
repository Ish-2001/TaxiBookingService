using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.Services.Interfaces
{
    public interface IBookingService
    {
        List<Booking> GetAll();
        List<DisplayBookingDTO> GetBookings();
        bool Add(BookingDTO newBooking);
        bool Update(BookingDTO updatedBooking, int id);
        List<DisplayBookingDTO> GetBookingHistory(string userName);

        bool PostRequest(int bookingId, string userName);
    }
}
