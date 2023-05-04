using AssetManagementSystemUI;
using BingMapsRESTToolkit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TaxiBookingServiceUI.Helper;
using TaxiBookingServiceUI.Models;
using TaxiBookingServiceUI.UnitOfWork;

namespace TaxiBookingServiceUI.Services
{
    public class DriverService
    {

        private readonly HelperAPI _helper = new HelperAPI();

        private string _token { get; set; }
        private string _drivertoken { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private readonly UnitOfWorkDriver _unitOfWork;

        public DriverService(IHttpContextAccessor httpContextAccessor,UnitOfWorkDriver unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _token = _session.GetString("usertoken");
            _drivertoken = _session.GetString("drivertoken");
            _unitOfWork = unitOfWork;
        }
        public HttpResponseMessage Login(LoginViewModel login)
        {
            HttpClient client = _helper.Initial();
            var postTask = client.PostAsJsonAsync<LoginViewModel>("Taxi-Booking-Service/driver/login", login);
            postTask.Wait();

            return postTask.Result;

        }

        public HttpResponseMessage GetLocation(string userName , LocationViewModel location)
        {
            HttpResponseMessage result = null;

            int userId = _unitOfWork.user.GetUsers().Result.Where(item => item.UserName == userName).Select(item => item.Id).FirstOrDefault();

            int? locationId = (GetAll().Result.Where(item => item.UserId == userId).Select(item => item.LocationId).FirstOrDefault());

           /* object o = locationId;
            int id = o == DBNull.Value ? 0 : (int)o;*/

            if (locationId != null)
            {              
                result = _unitOfWork.location.Edit(location, (int)locationId, userName);
            }
            else
            {
                result = _unitOfWork.location.Add(location, userName);
            }
            return result;
        }

        public async Task<List<DriverViewModel>> GetAll()
        {
            List<DriverViewModel> drivers = new List<DriverViewModel>();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            HttpResponseMessage message = await _helper.GetAsync(ApiUrls.DriverDisplay);

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                drivers = JsonConvert.DeserializeObject<List<DriverViewModel>>(result);

            }

            foreach(var driver in drivers)
            {
                UserViewModel user = _unitOfWork.user.GetUsers().Result.Where(item => item.Id == driver.UserId).FirstOrDefault();

                driver.PhoneNumber = user.PhoneNumber;

                driver.Password = user.Password;

                driver.FirstName= user.FirstName;

                driver.LastName = user.LastName;

                driver.VehicleCategory = _unitOfWork.vehicleCategory.GetAll().Result
                                          .Where(item => item.Id == driver.VehicleCategoryId)
                                          .Select(item => item.Type).FirstOrDefault();
                driver.VehicleName = _unitOfWork.vehicleDetail.GetAll().Result.Where(item => item.Id == driver.VehicleDetailId)
                                      .Select(item => item.Name).FirstOrDefault();
                driver.VehicleNumberPlate = _unitOfWork.vehicleDetail.GetAll().Result.Where(item => item.Id == driver.VehicleDetailId)
                                      .Select(item => item.RegisteredNumber).FirstOrDefault();
            }
            return drivers;

        }

        public DriverViewModel GetDriverById(int id)
        {
            return GetAll().Result.Where(item => item.Id == id).FirstOrDefault();
        }

        public async Task<List<DriverViewModel>> GetAvailableDrivers(BookingViewModel booking)
        {
            List<DriverViewModel> drivers = GetAll().Result.Where(item => item.IsActive == true).ToList();

            List<DriverViewModel> availableDrivers = new List<DriverViewModel>();

            foreach(var driver in drivers)
            {
                int locationId = driver.LocationId;
                LocationViewModel location = _unitOfWork.location.GetAll().Result.Where(item => item.Id == locationId).FirstOrDefault();

                double pickupLatitude = 0, pickupLongitude = 0 , latitude = 0 , longitude = 0;

                string address = booking.PickupLocation;

                PositionViewModel position = await PositionService.CalculateLocation(address);

                pickupLatitude = position.Latitude;
                pickupLongitude = position.Longitude;

                latitude = (double)location.Latitude;
                longitude = (double)location.Longitude;

                double distance = _unitOfWork.booking.CalculateDistance((double)latitude, (double)longitude, (double)pickupLatitude, (double)pickupLongitude);

                if (distance <= 10.00f)
                {
                    availableDrivers.Add(driver);
                }
            }

            return availableDrivers;
        }

        public async Task<bool> GetDriver(BookingViewModel booking , string userName)
        {

            HttpClient client = _helper.Initial();
            List<DriverViewModel> drivers = await GetAvailableDrivers(booking);

            int index = 0;

            while(index < drivers.Count)
            {

                booking.DriverId = drivers[index].Id;
                var message = new HttpRequestMessage(HttpMethod.Put, $"Taxi-Booking-Service/booking/postrequest/{booking.Id}/{userName}");
                var putTask = client.SendAsync(message);
                putTask.Wait();

                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    Thread.Sleep(30000);

                    var book = _unitOfWork.booking.GetBookingById(booking.Id);

                    if(book.Status == Status.Booked.ToString())
                    {
                        return true;
                    }
                    else
                    {
                        index++;
                    }
                }
            }

            booking.DriverId = null;

            return false;
        }

        public List<BookingViewModel> WaitingRequests(string username)
        {
            //decimal pickupLatitude = 0, pickupLongitude = 0;

            int userId = _unitOfWork.user.GetUsers().Result.Where(item => item.UserName == username).Select(item => item.Id).FirstOrDefault();

            int driverId = GetAll().Result.Where(item => item.UserId == userId).Select(item => item.Id).FirstOrDefault();
            int statusId = _unitOfWork.status.GetAll().Result.Where(item => item.Status == Status.Requested.ToString()).Select(item => item.Id).FirstOrDefault();

            var bookings = _unitOfWork.booking.GetAll().Result.Where(item => item.StatusId == statusId && item.DriverId == driverId).ToList();



           /* int locationId = GetAll().Result.Where(item => item.UserId == userId).Select(item => item.LocationId).FirstOrDefault();

            decimal latitude = _unitOfWork.location.GetAll().Result
                               .Where(item => item.Id == locationId).Select(item => item.Latitude).FirstOrDefault();
            decimal longitude = _unitOfWork.location.GetAll().Result
                               .Where(item => item.Id == locationId).Select(item => item.Longitude).FirstOrDefault();
*/
            /*List<BookingViewModel> result = new List<BookingViewModel>();

            foreach (var booking in bookings)
            {
                HttpClient httpClient = new HttpClient();

                string apiKey = "CUBhRBqPcYqsY8mz6inKzHqnG6pyF5yARXjxydVP0Pk";

                var address = booking.PickupLocation;

                string requestUrl = $"https://atlas.microsoft.com/search/address/json?api-version=1.0&subscription-key={apiKey}&query={Uri.EscapeDataString(address)}";

                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

                string responseJson = await response.Content.ReadAsStringAsync();
                JObject responseObj = JObject.Parse(responseJson);
                JToken positionToken = responseObj.SelectToken("$.results[0].position");

                pickupLatitude = (decimal)positionToken.SelectToken("lat");
                pickupLongitude = (decimal)positionToken.SelectToken("lon");

                double distance = _unitOfWork.booking.CalculateDistance((double)latitude, (double)longitude, (double)pickupLatitude, (double)pickupLongitude);

                if (distance <= 10.00f)
                {
                    result.Add(booking);
                }
            }*/

            return bookings;

        }

        public HttpResponseMessage Complaint(DriverComplaintViewModel newComplaint , string userName , string driverName)
        {
            UserViewModel user = _unitOfWork.user.GetUserByUserName(driverName);
            DriverViewModel driver = GetAll().Result.Where(item => item.UserId == user.Id).FirstOrDefault();
            user = _unitOfWork.user.GetUserByUserName (userName);
            BookingViewModel booking = _unitOfWork.booking.GetAll().Result
                                       .Where(item => item.UserId == user.Id && item.DriverId == driver.Id 
                                       && item.Status == Status.Completed.ToString()).LastOrDefault();

            DriverComplaintViewModel driverComplaint = new()
            {
                Complaint = newComplaint.Complaint,
                UserName= userName,
                DriverName= driverName,
            };

            HttpResponseMessage message = _helper.PostAsJsonAsync(ApiUrls.ComplaintAdd , driverComplaint , _drivertoken);

            return message;
        }


        public async Task<List<DriverComplaintViewModel>> GetComplaints()
        {
            List<DriverComplaintViewModel> driverComplaints = new List<DriverComplaintViewModel>();

            HttpResponseMessage message = await _helper.GetAsync(ApiUrls.DriverComplaintsDisplay);

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                driverComplaints = JsonConvert.DeserializeObject<List<DriverComplaintViewModel>>(result);

            }

            foreach (var driverComplaint in driverComplaints)
            {
                UserViewModel user = _unitOfWork.user.GetUsers().Result.Where(item => item.Id == driverComplaint.DriverId).FirstOrDefault();

                driverComplaint.NameDriver = user.FirstName + " " + user.LastName;

                user = _unitOfWork.user.GetUsers().Result.Where(item => item.Id == driverComplaint.UserId).FirstOrDefault();

                driverComplaint.NameUser = user.FirstName + " " + user.LastName;
            }

            return driverComplaints;

        }

    }
}
