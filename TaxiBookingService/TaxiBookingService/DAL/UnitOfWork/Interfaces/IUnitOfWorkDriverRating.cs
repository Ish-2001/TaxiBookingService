using TaxiBookingService.DAL.Repositories.Interfaces;
using TaxiBookingService.Data.Models;

namespace TaxiBookingService.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkDriverRating
    {
        public IUserRepository Users { get; }
        public IBookingRepository Bookings { get; }
        public IDriverRatingRepository Ratings { get; }
        public IDriverRepository Drivers { get; }
        public IBookingStatusRepository BookingsStatus { get; }

        public void Complete();
    }
}
