using TaxiBookingServiceUI.Services;

namespace TaxiBookingServiceUI.UnitOfWork
{
    public class UnitOfWorkBooking
    {
        public IHttpContextAccessor _httpContextAccessor;
        public UserService user;
        public PaymentModeService paymentMode;
        public VehicleCategoryService vehicleCategory;
        public StatusService status;
        public RideCancellationService rideCancellation;
        public PaymentService payment;

        public UnitOfWorkBooking(IHttpContextAccessor httpContextAccessor, UserService userService, PaymentModeService paymentModeService,
            VehicleCategoryService vehicleCategoryService, StatusService statusService, RideCancellationService rideCancellationService, PaymentService paymentService)
        {
            _httpContextAccessor = httpContextAccessor;
            user = userService;
            paymentMode = paymentModeService;
            vehicleCategory = vehicleCategoryService;
            status = statusService;
            rideCancellation = rideCancellationService;
            payment = paymentService;
        }
    }
}
