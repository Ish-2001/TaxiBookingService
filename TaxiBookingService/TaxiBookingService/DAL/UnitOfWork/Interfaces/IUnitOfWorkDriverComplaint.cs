using TaxiBookingService.DAL.Repositories.Interfaces;

namespace TaxiBookingService.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkDriverComplaint
    {
        public IUserRepository Users { get; }
        public IDriverRepository Drivers { get; }
        public IDriverComplaintRepository DriverComplaints { get; }
        public IBookingStatusRepository BookingsStatus { get; }
        public IBookingRepository Bookings { get; }
        public void Complete();
    }
}
