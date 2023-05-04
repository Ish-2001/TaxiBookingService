using TaxiBookingServiceUI.Helper;
using TaxiBookingServiceUI.Models;

namespace TaxiBookingServiceUI.Services
{
    public class AdminService
    {
        private readonly HelperAPI _helper = new HelperAPI();
        private string _token { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public AdminService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _token = _session.GetString("admintoken");
        }

        public HttpResponseMessage Login(LoginViewModel login)
        {
            HttpClient client = _helper.Initial();
            var postTask = client.PostAsJsonAsync<LoginViewModel>("Taxi-Booking-Service/admin/login", login);
            postTask.Wait();

            return postTask.Result;
        }

    }
}
