using TaxiBookingService.DAL.Repositories.Interfaces;

namespace TaxiBookingService.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkBooking
    {
        void Complete();
        public IUserRepository Users { get; }
        public IBookingRepository Bookings { get; }
        public IBookingStatusRepository BookingsStatus { get; }
        public IDriverRepository Drivers { get; }
        public IPaymentRepository Payments { get; }
        public IPaymentModeRepository PaymentModes { get; }
    }
}
