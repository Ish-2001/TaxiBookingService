using AssetManagementSystemUI;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using TaxiBookingServiceUI.Models;
using TaxiBookingServiceUI.Services;

namespace TaxiBookingServiceUI.Controllers
{
    public class DriverController : Controller
    {

        private readonly DriverService _driverService;
        private readonly StateService _stateService;
        private readonly BookingService _bookingService;
        private readonly LocationService _locationService;
        private readonly StatusService _statusService;
        private readonly VehicleDetailService _vehicleDetailService;
        private readonly VehicleCategoryService _vehicleCategoryService;
        private readonly IHttpContextAccessor _contextAccessor;
        public const string sessionToken = "drivertoken";
        public const string sessionName = "drivername";
        public const string bookingId = "bookingId";

        public DriverController(IHttpContextAccessor httpContextAccessor, DriverService driverService, StateService stateService, BookingService bookingService,
            LocationService locationService , StatusService statusService, VehicleDetailService vehicleDetailService, VehicleCategoryService vehicleCategoryService)
        {
            _driverService = driverService;
            _contextAccessor = httpContextAccessor;
            _bookingService = bookingService;
            _stateService = stateService;
            _locationService = locationService;
            _statusService = statusService;
            _vehicleDetailService = vehicleDetailService;
            _vehicleCategoryService = vehicleCategoryService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            HttpContext.Session.Remove("usertoken");
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            var result = _driverService.Login(login);

            if (result.IsSuccessStatusCode)
            {
                var token = result.Content.ReadAsStringAsync().Result;
                HttpContext.Session.SetString(sessionToken, token);
                HttpContext.Session.SetString(sessionName, login.UserName);
                return RedirectToAction("GetLocation");
            }
            TempData["message"] = "Invalid credentials.Please enter the credentials again";
            return View();
        }

        [HttpGet]
        public IActionResult GetLocation()
        {
            try
            {
                List<string> allStates = _stateService.GetStateNames();

                ViewBag.allStates = allStates;
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
           
        }

        [HttpPost]
        public IActionResult GetLocation(LocationViewModel location)
        {
            try
            {
                List<string> allStates = _stateService.GetStateNames();

                ViewBag.allStates = allStates;

                string userName = HttpContext.Session.GetString("drivername");

                var result = _driverService.GetLocation(userName, location);

                if (result.IsSuccessStatusCode)
                {
                    TempData["message"] = "location has been added successfully";
                    return RedirectToAction("WaitingRequests");
                }

                TempData["message"] = "Location with this id is already present in the system";

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult WaitingRequests()
        {
            try
            {
                string userName = HttpContext.Session.GetString("drivername");

                return View( _driverService.WaitingRequests(userName));
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }
        

        [HttpGet]
        public IActionResult AcceptRequest(int id)
        {
            try
            {
                string userName = HttpContext.Session.GetString("drivername");

                List<BookingViewModel> bookings = _bookingService.GetAcceptedRequest(id);

                foreach (var booking in bookings)
                {
                    if (booking.Status == Status.Cancelled.ToString())
                    {
                        return RedirectToAction("GetLocation");
                    }
                }

                var result = _bookingService.AcceptRequest(id, userName);

                if (result.IsSuccessStatusCode)
                {
                    //ViewBag.message = "Request has been accepted successfully";
                    return View(bookings);
                }

                ViewBag.message = "Request could not be accepted successfully";

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }

        

        public IActionResult Arrived(int id)
        {
            try
            {
                string userName = HttpContext.Session.GetString("drivername");

                BookingViewModel booking = _bookingService.GetBookingById(id);

                var result = _bookingService.Arrived(id, userName);

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("EnterOTP", booking);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }

        public IActionResult EnterOTP(BookingViewModel booking)
        {
            return View(booking);
        }

        [HttpPost]
        public IActionResult EnterOTP(string otp,int id)
        {
            try
            {
                string OTP = HttpContext.Session.GetString("otp");

                if (otp == OTP)
                {
                    TempData["message"] = "Ride has been started successfully";
                    return RedirectToAction("StartRide", new { id = id });
                }

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public IActionResult StartRide(int id)
        {
            try
            {
                string userName = HttpContext.Session.GetString("drivername");

                BookingViewModel booking = _bookingService.GetBookingById(id);

                var result = _bookingService.StartRide(id, userName);

                if (result.IsSuccessStatusCode)
                {
                    return View(booking);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }

        public IActionResult CompleteRide(int id)
        {
            try
            {
                string userName = HttpContext.Session.GetString("drivername");

                BookingViewModel booking = _bookingService.GetBookingById(id);

                var result = _bookingService.CompleteRide(id, userName);

                if (booking.PaymentMode == PaymentModes.Wallet.ToString())
                {
                    result = _bookingService.CompletePayment(id);

                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetLocation");
                    }
                    return View();
                }

                return RedirectToAction("PaymentVerification", booking);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }

        public IActionResult PaymentVerification(BookingViewModel booking)
        {
            return View(booking);
        }

        [HttpPost]
        public IActionResult PaymentVerification(string payment , int id)
        {
            try
            {
                if (payment == "0")
                {
                    return RedirectToAction("Complaint");
                }

                var result = _bookingService.CompletePayment(id);

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetLocation");
                }

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult Complaint()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Complaint(DriverComplaintViewModel driverComplaint)
        {
            string driverName = HttpContext.Session.GetString("drivername");
            string userName = HttpContext.Session.GetString("username");

            var result =_driverService.Complaint(driverComplaint, userName,driverName);

            if (result.IsSuccessStatusCode)
            {
                TempData["message"] = "Complaint has been filed successfully";
                return RedirectToAction("GetLocation");
            }

            TempData["message"] = "Complaint could not be filed";

            return View();
        }

        public async Task<IActionResult> AddVehicleDetails()
        {
            List<VehicleCategoryViewModel> categories = await _vehicleCategoryService.GetAll();

            List<string> categoryNames = new List<string>();

            foreach (var category in categories)
            {
                categoryNames.Add(category.Type);
            }

            ViewBag.categoryNames = categoryNames;

            return View();
        }

        public IActionResult AddVehicleDetails(VehicleDetailsViewModel newDetail)
        {
            string userName = HttpContext.Session.GetString("drivername");

            var result = _vehicleDetailService.Add(newDetail, userName);

            if (result.IsSuccessStatusCode)
            {
                ViewBag.message = "User Registered successfully";
                return RedirectToAction("Login");
            }
            return View();
        }

    }
}
