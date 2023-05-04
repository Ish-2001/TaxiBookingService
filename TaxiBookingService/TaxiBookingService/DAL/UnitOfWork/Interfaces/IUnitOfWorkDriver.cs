using TaxiBookingService.DAL.Repositories.Interfaces;

namespace TaxiBookingService.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkDriver : IDisposable
    {
        public IUserRepository Users { get; }
        public IUserRoleRepository UserRoles { get; }
        public IDriverRepository Drivers { get; }
        public IBookingRepository Bookings { get; }
        public IBookingStatusRepository BookingsStatus { get; }
        public IPaymentRepository Payments { get; }
        public IPaymentModeRepository PaymentModes { get; }
        public IRideCancellationRepository RideCancellations { get; }

        public void Complete();
    }
}
