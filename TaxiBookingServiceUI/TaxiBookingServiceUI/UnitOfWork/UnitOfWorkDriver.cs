using Microsoft.AspNetCore.Http;
using TaxiBookingServiceUI.Services;

namespace TaxiBookingServiceUI.UnitOfWork
{
    public class UnitOfWorkDriver
    {
        public UserService user;
        public LocationService location;
        public BookingService booking;
        public StatusService status;
        public VehicleCategoryService vehicleCategory;
        public VehicleDetailService vehicleDetail;

        public UnitOfWorkDriver( UserService userService,
            LocationService locationService, BookingService bookingService,
            StatusService statusService, VehicleCategoryService vehicleCategoryService, VehicleDetailService vehicleDetailService)
        {
            user = userService;
            location = locationService;
            booking = bookingService;
            status = statusService;
            vehicleCategory = vehicleCategoryService;
            vehicleDetail = vehicleDetailService;
        }
    }
}
