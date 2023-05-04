using TaxiBookingServiceUI.Services;

namespace TaxiBookingServiceUI.UnitOfWork
{
    public class UnitOfWorkCity
    {
        public IHttpContextAccessor _httpContextAccessor;
        public StateService state;

        public UnitOfWorkCity(IHttpContextAccessor httpContextAccessor, StateService stateService)
        {
            _httpContextAccessor = httpContextAccessor;
            state = stateService;
        }
    }
}
