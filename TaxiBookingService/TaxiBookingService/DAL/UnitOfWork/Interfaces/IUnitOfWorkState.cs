using TaxiBookingService.DAL.Repositories.Interfaces;

namespace TaxiBookingService.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkState
    {
        public IUserRepository Users { get; }
        public IStateRepository States { get; }
        public void Complete();
    }
}
