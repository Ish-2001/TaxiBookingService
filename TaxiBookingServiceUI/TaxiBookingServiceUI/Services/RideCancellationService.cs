using AssetManagementSystemUI;
using Newtonsoft.Json;
using System.Net.Http.Json;
using TaxiBookingServiceUI.Helper;
using TaxiBookingServiceUI.Models;

namespace TaxiBookingServiceUI.Services
{
    public class RideCancellationService
    {
        private readonly HelperAPI _helper = new HelperAPI();
        private string _token { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserService _userService;
        private readonly ISession _session;

        public RideCancellationService(IHttpContextAccessor httpContextAccessor, UserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _token = _session.GetString("admintoken");
            _userService = userService;
        }

        public async Task<List<RideCancellationViewModel>> GetAll()
        {
           
            List<RideCancellationViewModel> rideCancellations = new List<RideCancellationViewModel>();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            HttpResponseMessage message = await _helper.GetAsync(ApiUrls.RideCancellationDisplay);

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                rideCancellations = JsonConvert.DeserializeObject<List<RideCancellationViewModel>>(result);
            }
            return rideCancellations;
        }

        public HttpResponseMessage Add(RideCancellationViewModel rideCancellation, string userName , int bookingId)
        {
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            RideCancellationViewModel rideCancellationViewModel = new()
            {
                Reason = rideCancellation.Reason,
                UserName = userName,
                BookingId = bookingId
            };

            var message = _helper.PostAsJsonAsync(ApiUrls.RideCancellationAdd, rideCancellationViewModel , _token);
           
            return message;

        }

        public double CalculateRideFare(double distance)
        {
            double rideFare = 0;

            

            List<RideCancellationViewModel> cancelledRides =
                 GetAll().Result.Where(item => item.IsPending == true).ToList();

            foreach (var rideCancellation in cancelledRides)
            {
                bool isValid = _userService.GetCancellationReasons().Result.Where(item => item.Id == rideCancellation.ReasonId)
                               .Select(item => item.IsValid).FirstOrDefault();
                if(!isValid)
                    rideFare += rideCancellation.CancellationFee;
            }

            rideFare += distance * 25;

            return rideFare;
        }
    }
}
