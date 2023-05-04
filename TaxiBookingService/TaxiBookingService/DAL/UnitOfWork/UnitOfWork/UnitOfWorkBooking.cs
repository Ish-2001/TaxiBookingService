using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.DAL.Repositories.Repositories;
using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.UnitOfWork.UnitOfWork
{
    public class UnitOfWorkBooking : IUnitOfWorkBooking
    {
        private readonly TaxiContext _dBContext;

        public UnitOfWorkBooking(TaxiContext dbcontext)
        {
            _dBContext = dbcontext;
            Users = new UserRepository(_dBContext);
            Bookings = new BookingRepository(_dBContext);
            BookingsStatus = new BookingStatusRepository(_dBContext);
            Drivers = new DriverRepository(_dBContext);
            Payments = new PaymentRepository(_dBContext);
            PaymentModes = new PaymentModeRepository(_dBContext);
        }

        public IUserRepository Users { get; }
        public IBookingRepository Bookings { get; }
        public IBookingStatusRepository BookingsStatus { get; }
        public IDriverRepository Drivers { get; }
        public IPaymentRepository Payments { get; }
        public IPaymentModeRepository PaymentModes { get; }

        public void Complete()
        {
            _dBContext.SaveChanges();
        }
    }
}
