using Newtonsoft.Json;
using TaxiBookingServiceUI.Helper;
using TaxiBookingServiceUI.Models;

namespace TaxiBookingServiceUI.Services
{
    public class PaymentModeService
    {
        private readonly HelperAPI _helper = new HelperAPI();
        private string _token { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public PaymentModeService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _token = _session.GetString("admintoken");
        }

        public async Task<List<PaymentModeViewModel>> GetAll()
        {
            List<PaymentModeViewModel> paymentModes = new List<PaymentModeViewModel>();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            HttpResponseMessage message = await _helper.GetAsync(ApiUrls.PaymentModeDisplay);

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                paymentModes = JsonConvert.DeserializeObject<List<PaymentModeViewModel>>(result);
            }
            return paymentModes;
        }

        public List<string> GetModeNames()
        {
            List<PaymentModeViewModel> paymentModes = GetAll().Result;

            List<string> modeNames = new List<string>();

            foreach (var mode in paymentModes)
            {
                modeNames.Add(mode.Mode);
            }

            return modeNames;
        }
    }
}
