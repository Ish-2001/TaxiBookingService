using TaxiBookingService.DAL.Repositories.Interfaces;

namespace TaxiBookingService.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkCancellationReason
    {
        public IUserRepository Users { get; }
        public ICancellationReasonRepository CancellationReasons { get; }
        public void Complete();
    }
}
