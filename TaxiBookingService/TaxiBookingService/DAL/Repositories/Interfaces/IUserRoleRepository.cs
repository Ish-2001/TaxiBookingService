using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Interfaces
{
    public interface IUserRoleRepository : IGenericRepository<UserRole>
    {
        string GetRoleName(int roleId);
    }

}
