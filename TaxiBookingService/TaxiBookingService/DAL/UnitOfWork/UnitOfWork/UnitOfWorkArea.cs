using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.DAL.Repositories.Repositories;
using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.UnitOfWork.UnitOfWork
{
    public class UnitOfWorkArea : IUnitOfWorkArea
    {
        private readonly TaxiContext _dBContext;

        public UnitOfWorkArea(TaxiContext dbcontext)
        {
            _dBContext = dbcontext;
            Users = new UserRepository(_dBContext);
            Areas = new AreaRepository(_dBContext);
            Cities = new CityRepository(_dBContext);
        }

        public IUserRepository Users { get; }
        public IAreaRepository Areas { get; }
        public ICityRepository Cities { get; }

        public void Complete()
        {
            _dBContext.SaveChanges();
        }
    }
}
