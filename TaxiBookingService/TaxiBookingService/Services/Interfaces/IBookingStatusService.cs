using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.Services.Interfaces
{
    public interface IBookingStatusService
    {
        List<BookingStatus> GetAll();
        List<DisplayBookingStatusDTO> GetBookingStatus();
        bool Add(BookingStatusDTO newStatus);
        bool Update(BookingStatusDTO updatedStatus, int id);
        bool Delete(int id);
    }
}
