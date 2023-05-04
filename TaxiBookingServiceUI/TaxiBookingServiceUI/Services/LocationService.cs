using Newtonsoft.Json;
using System.Net.Http.Headers;
using TaxiBookingServiceUI.Helper;
using TaxiBookingServiceUI.Models;

namespace TaxiBookingServiceUI.Services
{
    public class LocationService
    {
        private readonly HelperAPI _helper = new HelperAPI();
        private string _token { get; set; }
        private string _drivertoken { get; set; }

        private readonly StateService _stateService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public LocationService(IHttpContextAccessor httpContextAccessor, StateService stateService)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _token = _session.GetString("admintoken");
            _drivertoken = _session.GetString("drivertoken");
            _stateService = stateService;
        }

        public async Task<List<LocationViewModel>> GetAll()
        {
            HttpClient client = _helper.Initial();
            List<LocationViewModel> locations = new List<LocationViewModel>();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            HttpResponseMessage message = await client.GetAsync("Taxi-Booking-Service/location/getall");

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                locations = JsonConvert.DeserializeObject<List<LocationViewModel>>(result);

            }
            return locations;

        }

        public HttpResponseMessage Add(LocationViewModel newLocation, string userName)
        {
            HttpClient client = _helper.Initial();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            LocationViewModel location = new()
            {
                Latitude = newLocation.Latitude,
                Longitude = newLocation.Longitude,
                State = newLocation.State,
                City = newLocation.City,
                Area = newLocation.Area,
                UserName = userName
            };

            var postTask = client.PostAsJsonAsync<LocationViewModel>("Taxi-Booking-Service/location/add", location);
            postTask.Wait();

            return postTask.Result;

        }

        public async void Delete(int id)
        {

            HttpClient client = _helper.Initial();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            HttpResponseMessage message = await client.DeleteAsync($"Taxi-Booking-Service/city/delete/{id}");
        }

        public HttpResponseMessage Edit(LocationViewModel updatedLocation, int id , string userName )
        {
            HttpClient client = _helper.Initial();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _drivertoken);

            LocationViewModel location = new()
            {
                Latitude = updatedLocation.Latitude,
                Longitude = updatedLocation.Longitude,
                State = updatedLocation.State,
                City = updatedLocation.City,
                Area = updatedLocation.Area,
                UserName = userName
            };

            var putTask = client.PutAsJsonAsync<LocationViewModel>($"Taxi-Booking-Service/location/update/{id}", location);

            putTask.Wait();

            return putTask.Result;
        }
    }
}
