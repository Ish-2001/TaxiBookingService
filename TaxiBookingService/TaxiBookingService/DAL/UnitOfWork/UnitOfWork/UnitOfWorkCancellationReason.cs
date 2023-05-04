using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.DAL.Repositories.Repositories;
using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.UnitOfWork.UnitOfWork
{
    public class UnitOfWorkCancellationReason : IUnitOfWorkCancellationReason
    {
        private readonly TaxiContext _dBContext;

        public UnitOfWorkCancellationReason(TaxiContext dbcontext)
        {
            _dBContext = dbcontext;
            Users = new UserRepository(_dBContext);
            CancellationReasons = new CancellationReasonRepository(_dBContext);
        }

        public IUserRepository Users { get; }
        public ICancellationReasonRepository CancellationReasons { get; }

        public void Complete()
        {
            _dBContext.SaveChanges();
        }
    }
}
