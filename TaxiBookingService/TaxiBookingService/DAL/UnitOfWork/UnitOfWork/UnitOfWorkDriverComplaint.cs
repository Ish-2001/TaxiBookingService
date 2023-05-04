using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.DAL.Repositories.Repositories;
using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Service;

namespace TaxiBookingService.DAL.UnitOfWork.UnitOfWork
{
    public class UnitOfWorkDriverComplaint : IUnitOfWorkDriverComplaint
    {
        private readonly TaxiContext _dBContext;

        public UnitOfWorkDriverComplaint(TaxiContext dbcontext)
        {
            _dBContext = dbcontext;
            Users = new UserRepository(_dBContext);
            Drivers = new DriverRepository(_dBContext);
            DriverComplaints = new DriverComplaintRepository(_dBContext);
            Bookings = new BookingRepository(_dBContext);
            BookingsStatus = new BookingStatusRepository(_dBContext);
        }

        public IUserRepository Users { get; }
        public IDriverRepository Drivers { get; }
        public IDriverComplaintRepository DriverComplaints { get; }
        public IBookingStatusRepository BookingsStatus { get; }
        public IBookingRepository Bookings { get; }

        public void Complete()
        {
            _dBContext.SaveChanges();
        }
    }
}
