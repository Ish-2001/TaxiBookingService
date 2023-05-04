using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.DAL.Repositories.Repositories;
using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.UnitOfWork.UnitOfWork
{
    public class UnitOfWorkBookingStatus : IUnitOfWorkBookingStatus
    {
        private readonly TaxiContext _dBContext;

        public UnitOfWorkBookingStatus(TaxiContext dbcontext)
        {
            _dBContext = dbcontext;
            Users = new UserRepository(_dBContext);
            BookingsStatus = new BookingStatusRepository(_dBContext);
        }
        public IBookingStatusRepository BookingsStatus { get; }
        public IUserRepository Users { get; }

        public void Complete()
        {
            _dBContext.SaveChanges();
        }
    }
}
