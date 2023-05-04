using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Services.Service
{
    public class DriverRatingService : IDriverRatingService
    {
        private readonly IUnitOfWorkDriverRating _unitOfWork;

        public DriverRatingService(IUnitOfWorkDriverRating unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Add(DriverRatingDTO driverRating )
        {
            User user = _unitOfWork.Users.Get(item => item.UserName == driverRating.UserName);
            //BookingStatus status = _unitOfWork.BookingsStatus.Get(item => item.Status == Status.Completed.ToString());
            Booking booking = _unitOfWork.Bookings.Get(item => item.Id == driverRating.BookingId);
            Driver driver = _unitOfWork.Drivers.Get(item => item.Id == booking.DriverId);

            if (user == null) return false;
          
            if (booking == null) return false;


            DriverRating rating = new()
            {
                Rating = driverRating.Rating,
                UserId = user.Id,
                DriverId = (int)booking.DriverId,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                CreatedBy = user.Id
            };


            driver.Rating = (driver.Rating + driverRating.Rating)/2;

            _unitOfWork.Drivers.Update(driver);
            _unitOfWork.Ratings.Add(rating);
            _unitOfWork.Complete();

            return true;
        }
    }
}
