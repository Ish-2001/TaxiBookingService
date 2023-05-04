using Newtonsoft.Json;
using System.Net.Http.Headers;
using TaxiBookingServiceUI.Helper;
using TaxiBookingServiceUI.Models;

namespace TaxiBookingServiceUI.Services
{
    public class StateService
    {
        private readonly HelperAPI _helper = new HelperAPI();
        private string _token { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public StateService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _token = _session.GetString("admintoken");
        }

        public async Task<List<StateViewModel>> GetAll()
        {
            HttpClient client = _helper.Initial();
            List<StateViewModel> states = new List<StateViewModel>();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            HttpResponseMessage message = await client.GetAsync("Taxi-Booking-Service/state/getall");

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                states = JsonConvert.DeserializeObject<List<StateViewModel>>(result);
            }
            return states;
        }

        public HttpResponseMessage Add(StateViewModel newState, string userName)
        {
            HttpClient client = _helper.Initial();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            StateViewModel state = new()
            {
                Name = newState.Name,
                //State = newCity.State,
                UserName = userName
            };

            var postTask = client.PostAsJsonAsync<StateViewModel>("Taxi-Booking-Service/state/Add", state);
            postTask.Wait();

            return postTask.Result;

        }

        public List<string> GetStateNames()
        {
            List<StateViewModel> states = GetAll().Result;

            List<string> allStates = new List<string>();

            foreach (var state in states)
            {
                allStates.Add(state.Name);
            }
           
            return allStates;
        }

        public async void Delete(int id)
        {

            HttpClient client = _helper.Initial();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            HttpResponseMessage message = await client.DeleteAsync($"Taxi-Booking-Service/state/delete/{id}");
        }

        public HttpResponseMessage Edit(StateViewModel updatedState, int id)
        {
            HttpClient client = _helper.Initial();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            StateViewModel state = new()
            {
                Name = updatedState.Name
            };

            var putTask = client.PutAsJsonAsync<StateViewModel>($"Taxi-Booking-Service/state/update/{id}", state);

            putTask.Wait();

            return putTask.Result;
        }

    }
}
