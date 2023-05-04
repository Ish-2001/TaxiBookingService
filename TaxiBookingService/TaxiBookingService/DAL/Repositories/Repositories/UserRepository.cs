using Microsoft.EntityFrameworkCore;
using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.Repositories.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(TaxiContext _context) : base(_context)
        {

        }

        public string GetFullName(int userId)
        {
            return FindAll(item => item.Id == userId).Select(item => item.FirstName).FirstOrDefault() + " " +
                FindAll(item => item.Id == userId).Select(item => item.LastName).FirstOrDefault();
        }

        public List<User> GetAll()
        {
            return FindAll(item => item.IsDeleted == false).Include(item => item.UserRole).ToList();
        }
    }
}
