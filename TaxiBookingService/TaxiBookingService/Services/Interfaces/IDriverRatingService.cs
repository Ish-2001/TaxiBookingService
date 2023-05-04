using TaxiBookingService.Data.Domain;

namespace TaxiBookingService.Services.Interfaces
{
    public interface IDriverRatingService
    {
        bool Add(DriverRatingDTO driverRating);
    }
}
