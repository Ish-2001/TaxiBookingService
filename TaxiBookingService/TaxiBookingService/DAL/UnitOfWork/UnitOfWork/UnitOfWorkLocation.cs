using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.DAL.Repositories.Repositories;
using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.UnitOfWork.UnitOfWork
{
    public class UnitOfWorkLocation : IUnitOfWorkLocation
    {
        private readonly TaxiContext _dBContext;

        public UnitOfWorkLocation(TaxiContext dbcontext)
        {
            _dBContext = dbcontext;
            Users = new UserRepository(_dBContext);
            Areas = new AreaRepository(_dBContext);
            Cities = new CityRepository(_dBContext);
            States = new StateRepository(_dBContext);
            Drivers = new DriverRepository(_dBContext);
            Locations = new LocationRepository(_dBContext);
        }

        public IUserRepository Users { get; }
        public IAreaRepository Areas { get; }
        public ICityRepository Cities { get; }
        public IStateRepository States { get; }
        public IDriverRepository Drivers { get; }
        public ILocationRepository Locations { get; }

        public void Complete()
        {
            _dBContext.SaveChanges();
        }
    }
}
