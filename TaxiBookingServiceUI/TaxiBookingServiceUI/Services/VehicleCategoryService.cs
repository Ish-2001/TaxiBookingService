using Newtonsoft.Json;
using TaxiBookingServiceUI.Helper;
using TaxiBookingServiceUI.Models;

namespace TaxiBookingServiceUI.Services
{
    public class VehicleCategoryService
    {
        private readonly HelperAPI _helper = new HelperAPI();
        private string _token { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public VehicleCategoryService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _token = _session.GetString("admintoken");
        }

        public async Task<List<VehicleCategoryViewModel>> GetAll()
        {
            List<VehicleCategoryViewModel> cities = new List<VehicleCategoryViewModel>();

            HttpResponseMessage message = await _helper.GetAsync(ApiUrls.VehicleCategoryDisplay);

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                cities = JsonConvert.DeserializeObject<List<VehicleCategoryViewModel>>(result);
            }
            return cities;
        }

        public List<string> GetCategoryNames()
        {
            List<VehicleCategoryViewModel> vehicleCategories = GetAll().Result;

            List<string> categoryNames = new List<string>();

            foreach (var category in vehicleCategories)
            {
                categoryNames.Add(category.Type);
            }
            return categoryNames;

        }
    }
}
