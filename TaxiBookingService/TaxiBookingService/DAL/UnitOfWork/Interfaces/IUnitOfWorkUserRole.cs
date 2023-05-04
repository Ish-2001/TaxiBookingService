using TaxiBookingService.DAL.Repositories.Interfaces;

namespace TaxiBookingService.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkUserRole
    {
        public IUserRepository Users { get; }
        public IUserRoleRepository UserRoles { get; }
        public void Complete();
    }
}
