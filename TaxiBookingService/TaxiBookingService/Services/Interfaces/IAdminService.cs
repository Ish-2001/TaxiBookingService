using TaxiBookingService.Data.Domain;

namespace TaxiBookingService.Services.Interfaces
{
    public interface IAdminService
    {
        bool Login(LoginDTO login);
        bool ApproveDriver(string userName);
    }
}
