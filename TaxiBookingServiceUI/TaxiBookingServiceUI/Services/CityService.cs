using Newtonsoft.Json;
using NuGet.ContentModel;
using System.Net.Http.Headers;
using System.Security.Policy;
using TaxiBookingServiceUI.Helper;
using TaxiBookingServiceUI.Models;
using TaxiBookingServiceUI.UnitOfWork;

namespace TaxiBookingServiceUI.Services
{
    public class CityService
    {
        private readonly HelperAPI _helper = new HelperAPI();
        private string _token { get; set; }

        private readonly UnitOfWorkCity _unitOfWork;
        private readonly ISession _session;

        public CityService(IHttpContextAccessor httpContextAccessor , StateService stateService , UnitOfWorkCity unitOfWork)
        {
           /* _httpContextAccessor = httpContextAccessor;*/
            _session = httpContextAccessor.HttpContext.Session;
            _token = _session.GetString("admintoken");
            //_stateService = stateService;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CityViewModel>> GetAll()
        {
            List<CityViewModel> cities = new List<CityViewModel>();

            HttpResponseMessage message = await _helper.GetAsync(ApiUrls.CityDisplay);

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                cities = JsonConvert.DeserializeObject<List<CityViewModel>>(result);

            }

            foreach(var item in cities)
            {
                item.State = _unitOfWork.state.GetAll().Result.Where(i => i.Id == item.StateId).Select(i => i.Name).FirstOrDefault();
            }

            return cities;
        }

        public List<string> GetCities(string filter)
        {
            if (filter == null)
                return null;

            var cities = GetAll().Result.Where(item => item.State == filter)
                  .Select(item => item.Name)
                  .Distinct()
                  .ToList();

            return cities;
        }

        public List<string> GetCityNames()
        {
            List<CityViewModel> cities = GetAll().Result;

            List<string> allCities = new List<string>();

            foreach (var city in cities)
            {
                allCities.Add(city.Name);
            }

            return allCities;
        }

        public HttpResponseMessage Add(CityViewModel newCity,string userName)
        {
  
            CityViewModel city = new()
            {
                Name = newCity.Name,
                State = newCity.State,
                UserName = userName
            };

            HttpResponseMessage message = _helper.PostAsJsonAsync(ApiUrls.CityAdd, city , _token);

            return message;

        }

        public async Task<HttpResponseMessage> Delete(int id)
        {

            HttpClient client = _helper.Initial();

            HttpResponseMessage message = await client.DeleteAsync($"Taxi-Booking-Service/city/delete/{id}");

            return message;
        }

        public HttpResponseMessage Edit(CityViewModel updatedCity, int id)
        {

            CityViewModel city = new()
            {
                Name = updatedCity.Name
            };

            var message = _helper.PutAsJsonAsync(ApiUrls.CityUpdate,city,id,_token);

            return message;
        }
    }
}
