using Newtonsoft.Json;
using TaxiBookingServiceUI.Helper;
using TaxiBookingServiceUI.Models;

namespace TaxiBookingServiceUI.Services
{
    public class AreaService
    {
        private readonly HelperAPI _helper = new HelperAPI();
        private string _token { get; set; }

        private readonly CityService _cityService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public AreaService(IHttpContextAccessor httpContextAccessor, CityService cityService)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _token = _session.GetString("admintoken");
            _cityService = cityService;
        }

        public async Task<List<AreaViewModel>> GetAll()
        {
            List<AreaViewModel> areas = new List<AreaViewModel>();

            HttpResponseMessage message = await _helper.GetAsync(ApiUrls.AreaDisplay);

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                areas = JsonConvert.DeserializeObject<List<AreaViewModel>>(result);

            }

            foreach (var item in areas)
            {
                item.City = _cityService.GetAll().Result.Where(i => i.Id == item.CityId).Select(i => i.Name).FirstOrDefault();
            }

            return areas;
        }

        public List<string> GetAreas(string filter)
        {
            if (filter == null)
                return null;

            var areas = GetAll().Result.Where(item => item.City == filter)
                  .Select(item => item.Name)
                  .Distinct()
                  .ToList();

            return areas;
        }

        public HttpResponseMessage Add(AreaViewModel newArea, string userName)
        {
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            AreaViewModel area = new()
            {
                Name = newArea.Name,
                City = newArea.City,
                Locality = newArea.Locality,
                UserName = userName
            };

            HttpResponseMessage message = _helper.PostAsJsonAsync(ApiUrls.AreaAdd , area , _token);

            return message;

        }

        public async void Delete(int id)
        {
            HttpResponseMessage message = await _helper.DeleteAsync(ApiUrls.AreaDelete, id);
        }

        public HttpResponseMessage Edit(AreaViewModel updatedArea, int id)
        {
            HttpClient client = _helper.Initial();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            AreaViewModel area = new()
            {
                Name = updatedArea.Name
            };

            var putTask = client.PutAsJsonAsync<AreaViewModel>($"Taxi-Booking-Service/area/update/{id}", area);

            putTask.Wait();

            return putTask.Result;
        }
    }
}
