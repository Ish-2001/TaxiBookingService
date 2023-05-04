using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        string GetFullName(int userId);
        List<User> GetAll();
    }
}
