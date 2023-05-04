using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.DAL.Repositories.Repositories;
using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.UnitOfWork.UnitOfWork
{
    public class UnitOfWorkDriver : IUnitOfWorkDriver
    {
        private readonly TaxiContext _dBContext;

        public UnitOfWorkDriver(TaxiContext dbcontext)
        {
            _dBContext = dbcontext;
            Users = new UserRepository(_dBContext);
            UserRoles = new UserRoleRepository(_dBContext);
            Drivers = new DriverRepository(_dBContext);
            Bookings = new BookingRepository(_dBContext);
            BookingsStatus = new BookingStatusRepository(_dBContext);
            Payments = new PaymentRepository(_dBContext);
            PaymentModes = new PaymentModeRepository(_dBContext);
            RideCancellations = new RideCancellationRepository(_dBContext);
        }

        public IUserRepository Users { get; }
        public IUserRoleRepository UserRoles { get; }
        public IDriverRepository Drivers { get; }
        public IBookingRepository Bookings { get; }
        public IBookingStatusRepository BookingsStatus { get; }
        public IPaymentRepository Payments { get; }
        public IPaymentModeRepository PaymentModes { get; }
        public IRideCancellationRepository RideCancellations { get; }

        public void Complete()
        {
            _dBContext.SaveChanges();
        }

        public void Dispose()
        {
            _dBContext.Dispose();
        }
    }
}
