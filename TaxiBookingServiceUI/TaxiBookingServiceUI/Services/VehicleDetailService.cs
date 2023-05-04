using Newtonsoft.Json;
using TaxiBookingServiceUI.Helper;
using TaxiBookingServiceUI.Models;

namespace TaxiBookingServiceUI.Services
{
    public class VehicleDetailService
    {
        private readonly HelperAPI _helper = new HelperAPI();
        private string _token { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public VehicleDetailService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _token = _session.GetString("admintoken");
        }

        public async Task<List<VehicleDetailsViewModel>> GetAll()
        {
            List<VehicleDetailsViewModel> details = new List<VehicleDetailsViewModel>();

            HttpResponseMessage message = await _helper.GetAsync(ApiUrls.VehicleDetailDisplay);

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                details = JsonConvert.DeserializeObject<List<VehicleDetailsViewModel>>(result);
            }

            return details;
        }

        public HttpResponseMessage Add(VehicleDetailsViewModel newDetail, string userName)
        {
            VehicleDetailsViewModel detail = new()
            {
                Name = newDetail.Name,
                ModelNumber = newDetail.ModelNumber,
                RegisteredNumber = newDetail.RegisteredNumber,
                RegisteredName = newDetail.RegisteredName,
                NumberOfSeats = newDetail.NumberOfSeats,
                Type = newDetail.Type,
                UserName = userName
            };

            var message = _helper.PostAsJsonAsync(ApiUrls.VehicleDetailAdd, detail,_token);
            return message;

        }
    }
}
