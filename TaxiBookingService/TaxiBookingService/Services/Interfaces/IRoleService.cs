using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.Services.Interfaces
{
    public interface IRoleService
    {
        List<UserRole> GetAll();
        List<DisplayRolesDTO> GetRoles();
        bool Add(UserRoleDTO newRole);
        bool Update(UserRoleDTO updatedRole, int id);
        bool Delete(int id);
    }
}
