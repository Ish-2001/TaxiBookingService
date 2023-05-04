using TaxiBookingService.DAL.Repositories.Interfaces;

namespace TaxiBookingService.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkUser
    {
        public IUserRepository Users { get; }
        public IUserRoleRepository UserRoles { get; }
        public IDriverRepository Drivers { get; }
        public void Complete();
    }
}
