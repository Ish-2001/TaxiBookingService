using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Repositories
{
    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(TaxiContext _context) : base(_context)
        {
        }

        public string GetRoleName(int roleId)
        {
            return FindAll(item => item.Id == roleId).Select(item => item.Role).FirstOrDefault();
        }
    }
}
