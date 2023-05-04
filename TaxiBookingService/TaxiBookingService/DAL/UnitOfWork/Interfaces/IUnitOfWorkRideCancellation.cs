using TaxiBookingService.DAL.Repositories.Interfaces;

namespace TaxiBookingService.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkRideCancellation
    {
        public IUserRepository Users { get; }
        public IRideCancellationRepository RideCancellations { get; }
        public IBookingRepository Bookings { get; }
        public IPaymentRepository Payments { get; }
        public ICancellationReasonRepository CancellationReasons { get; }
        public IBookingStatusRepository BookingsStatus { get; }
        public void Complete();
    }
}
