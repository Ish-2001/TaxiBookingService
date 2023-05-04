using TaxiBookingService.DAL.Repositories.Interfaces;

namespace TaxiBookingService.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkLocation
    {
        public IUserRepository Users { get; }
        public IAreaRepository Areas { get; }
        public ICityRepository Cities { get; }
        public IStateRepository States { get; }
        public IDriverRepository Drivers { get; }
        public ILocationRepository Locations { get; }

        public void Complete();
    }
}
