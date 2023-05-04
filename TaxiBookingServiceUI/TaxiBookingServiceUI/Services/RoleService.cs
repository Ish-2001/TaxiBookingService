using Newtonsoft.Json;
using TaxiBookingServiceUI.Helper;
using TaxiBookingServiceUI.Models;

namespace TaxiBookingServiceUI.Services
{
    public class RoleService
    {
        private readonly HelperAPI _helper = new HelperAPI();
        private string _token { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public RoleService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _token = _session.GetString("admintoken");
        }

        public async Task<List<RoleViewModel>> GetAll()
        {
            HttpClient client = _helper.Initial();
            List<RoleViewModel> roles = new List<RoleViewModel>();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            HttpResponseMessage message = await client.GetAsync("Taxi-Booking-Service/role/getall");

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                roles = JsonConvert.DeserializeObject<List<RoleViewModel>>(result);
            }
            return roles;
        }

        public List<string> GetRoleNames()
        {
            List<RoleViewModel> roles = GetAll().Result;

            List<string> roleNames = new List<string>();

            foreach (var role in roles)
            {
                roleNames.Add(role.Role);
            }

            return roleNames;
        }

        public HttpResponseMessage Add(RoleViewModel newRole, string userName)
        {
            HttpClient client = _helper.Initial();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            RoleViewModel role = new()
            {
                Role = newRole.Role,
                UserName = userName
            };

            var postTask = client.PostAsJsonAsync<RoleViewModel>("Taxi-Booking-Service/role/Add", role);
            postTask.Wait();

            return postTask.Result;

        }

        public async void Delete(int id)
        {

            HttpClient client = _helper.Initial();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            HttpResponseMessage message = await client.DeleteAsync($"Taxi-Booking-Service/role/delete/{id}");
        }

        public HttpResponseMessage Edit(RoleViewModel updatedRole, int id)
        {
            HttpClient client = _helper.Initial();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            RoleViewModel role = new()
            {
                Role = updatedRole.Role
            };

            var putTask = client.PutAsJsonAsync<RoleViewModel>($"Taxi-Booking-Service/role/update/{id}", role);

            putTask.Wait();

            return putTask.Result;
        }
    }
}
