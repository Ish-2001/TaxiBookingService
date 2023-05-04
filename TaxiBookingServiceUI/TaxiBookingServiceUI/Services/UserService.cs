using AssetManagementSystemUI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.ContentModel;
using System.Linq;
using System.Net.Http.Headers;
using TaxiBookingServiceUI.Helper;
using TaxiBookingServiceUI.Models;

namespace TaxiBookingServiceUI.Services
{
    public class UserService
    {
        private readonly HelperAPI _helper = new HelperAPI();

        private string _token { get; set; }
        public string userName { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleService _roleService;
        private readonly ISession _session;

        public UserService(IHttpContextAccessor httpContextAccessor, RoleService roleService)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _token = _session.GetString("usertoken");
            userName = _session.GetString("username");
            _roleService = roleService;
        }


        public HttpResponseMessage Add(UserViewModel newUser)
        {
            HttpClient client = _helper.Initial();

            UserViewModel user = new()
            {
                LastName = newUser.LastName,
                FirstName = newUser.FirstName,
                Email = newUser.Email,
                Password = newUser.Password,
                PhoneNumber = newUser.PhoneNumber,
                Gender = newUser.Gender,
                UserName = newUser.UserName,
                Role = Role.User.ToString(),
            };


            var message = _helper.PostAsJsonAsync(ApiUrls.UserAdd, user , _token);

            return message;
        }

        public HttpResponseMessage Update(string userName , UserViewModel updatedUser)
        {
            UserViewModel userViewModel = GetUsers().Result.Where(item => item.UserName == userName).FirstOrDefault();

            UserViewModel user = new()
            {
                LastName = updatedUser.LastName,
                FirstName = updatedUser.FirstName,
                Email = updatedUser.Email,
                PhoneNumber = updatedUser.PhoneNumber,
                Gender = updatedUser.Gender,
                UserName = updatedUser.UserName,
                Role = updatedUser.Role,
                Balance = updatedUser.Balance + userViewModel.Balance
            };

            HttpResponseMessage message = _helper.PutAsJsonAsync(ApiUrls.UserUpdate,user, userViewModel.Id, _token);
            return message;
        }

        public HttpResponseMessage Login(LoginViewModel login)
        {
            HttpClient client = _helper.Initial();
            var message = _helper.PostAsJsonAsync(ApiUrls.UserLogin, login,_token);

            return message;
        }

        public async Task<List<UserViewModel>> GetUsers()
        {
            List<UserViewModel> users = new List<UserViewModel>();

            HttpResponseMessage message = await _helper.GetAsync(ApiUrls.UserDisplay);

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<UserViewModel>>(result);
            }

            foreach (var user in users)
            {
                user.Role = _roleService.GetAll().Result.Where(item => item.Id == user.RoleId).Select(item => item.Role).FirstOrDefault();
            }
            return users;
        }

        public UserViewModel GetUserByUserName(string UserName)
        {
            return GetUsers().Result.Where(item => item.UserName == UserName).FirstOrDefault();
        }

        public async Task<List<CancellationReasonsViewModel>> GetCancellationReasons()
        {
            HttpClient client = _helper.Initial();
            List<CancellationReasonsViewModel> reasons = new List<CancellationReasonsViewModel>();

            HttpResponseMessage message = await client.GetAsync(ApiUrls.CancellationReasonDisplay);

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                reasons = JsonConvert.DeserializeObject<List<CancellationReasonsViewModel>>(result);
            }

            return reasons;
        }

        public List<string> GetReasonNames()
        {
            List<CancellationReasonsViewModel> reasons = GetCancellationReasons().Result;

            List<string> reasonNames = new List<string>();

            foreach (var reason in reasons)
            {
                reasonNames.Add(reason.Reason);
            }

            return reasonNames;
        }
    }
}
