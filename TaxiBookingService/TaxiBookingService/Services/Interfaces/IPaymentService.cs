using TaxiBookingService.Data.Models;

namespace TaxiBookingService.Services.Interfaces
{
    public interface IPaymentService
    {
        List<Payment> GetAll();
        bool Add(Booking booking);
    }
}
