using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.DAL.Repositories.Repositories;
using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.UnitOfWork.UnitOfWork
{
    public class UnitOfWorkUserRole : IUnitOfWorkUserRole
    {
        private readonly TaxiContext _dBContext;

        public UnitOfWorkUserRole(TaxiContext dbcontext)
        {
            _dBContext = dbcontext;
            Users = new UserRepository(_dBContext);
            UserRoles = new UserRoleRepository(_dBContext);
        }

        public IUserRepository Users { get; }
        public IUserRoleRepository UserRoles { get; }
        public void Complete()
        {
            _dBContext.SaveChanges();
        }
    }
}
