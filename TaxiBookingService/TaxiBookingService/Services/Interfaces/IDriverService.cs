using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.Services.Interfaces
{
    public interface IDriverService
    {
        List<Driver> GetAll();
        List<DisplayDriverDTO> GetDrivers();
        bool Add(DriverDTO newDriver);
        bool Login(LoginDTO login);
        bool CompletePayment(int id, PaymentDTO updatedPayment);
        /*bool Update(AreaDTO updatedArea, int id);
        bool Delete(int id);*/
    }
}
