using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetAll();
        List<DisplayUserDTO> GetUsers();
        bool Add(UserDTO newUser);
        User AddUser(DriverDTO newUser, UserRole userRole);
        bool Update(UpdateUserDTO updatedAsset, int id);
        bool Delete(int id);
        bool Login(LoginDTO login);
    }
}
