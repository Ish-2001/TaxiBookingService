using Microsoft.EntityFrameworkCore;
using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.DAL.Repositories.Repositories;
using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.UnitOfWork.UnitOfWork
{
    public class UnitOfWorkRideCancellation : IUnitOfWorkRideCancellation
    {
        private readonly TaxiContext _dBContext;

        public UnitOfWorkRideCancellation(TaxiContext dbcontext)
        {
            _dBContext = dbcontext;
            Users = new UserRepository(_dBContext);
            Bookings = new BookingRepository(_dBContext);
            RideCancellations = new RideCancellationRepository(_dBContext);
            Payments = new PaymentRepository(_dBContext);
            CancellationReasons = new CancellationReasonRepository(_dBContext);
            BookingsStatus = new BookingStatusRepository(_dBContext);
        }

        public IUserRepository Users { get; }
        public IRideCancellationRepository RideCancellations { get; }
        public IBookingRepository Bookings { get; }
        public IPaymentRepository Payments { get; }
        public ICancellationReasonRepository CancellationReasons { get; }
        public IBookingStatusRepository BookingsStatus { get; }
        public void Complete()
        {
            _dBContext.SaveChanges();
        }
    }
}
