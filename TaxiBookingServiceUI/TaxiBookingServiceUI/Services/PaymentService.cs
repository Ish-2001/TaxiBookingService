using Newtonsoft.Json;
using TaxiBookingServiceUI.Helper;
using TaxiBookingServiceUI.Models;

namespace TaxiBookingServiceUI.Services
{
    public class PaymentService
    {
        private readonly HelperAPI _helper = new HelperAPI();
        private string _token { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public PaymentService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _token = _session.GetString("admintoken");
        }

        public async Task<List<PaymentViewModel>> GetAll()
        {
            List<PaymentViewModel> payments = new List<PaymentViewModel>();

            HttpResponseMessage message = await _helper.GetAsync(ApiUrls.PaymentDisplay);

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                payments = JsonConvert.DeserializeObject<List<PaymentViewModel>>(result);

            }

            return payments;
        }
    }
}
