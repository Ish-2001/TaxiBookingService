using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.DAL.Repositories.Repositories;
using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.UnitOfWork.UnitOfWork
{
    public class UnitOfWorkCity : IUnitOfWorkCity
    {
        private readonly TaxiContext _dBContext;

        public UnitOfWorkCity(TaxiContext dbcontext)
        {
            _dBContext = dbcontext;
            Users = new UserRepository(_dBContext);
            Cities = new CityRepository(_dBContext);
            States = new StateRepository(_dBContext);
        }

        public IUserRepository Users { get; }
        public ICityRepository Cities { get; }
        public IStateRepository States { get; }

        public void Complete()
        {
            _dBContext.SaveChanges();
        }
    }
}
