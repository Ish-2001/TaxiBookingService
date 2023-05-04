using AssetManagementSystemUI;
using Microsoft.AspNetCore.Mvc;
using TaxiBookingServiceUI.Models;
using TaxiBookingServiceUI.Services;
//using TaxiBookingServiceUI.Services.User;

namespace TaxiBookingServiceUI.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly StateService _stateService;
        private readonly BookingService _bookingService;
        private readonly RoleService _roleService;
        private readonly PaymentModeService _paymentModeService;
        private readonly VehicleCategoryService _vehicleCategoryService;
        private readonly RideCancellationService _rideCancellationService;
        private readonly DriverService _driverService;
        private readonly IHttpContextAccessor _contextAccessor;
        public const string sessionToken = "usertoken";
        public const string sessionId = "username";
        public const string otp = "otp";

        public UserController(IHttpContextAccessor httpContextAccessor, UserService userService , 
                             BookingService bookingService , StateService stateService
                             , RoleService roleService , PaymentModeService paymentModeService , 
                             VehicleCategoryService vehicleCategoryService , RideCancellationService rideCancellationService,
                             DriverService driverService)
        {
            _userService = userService;
            _contextAccessor = httpContextAccessor;
            _bookingService = bookingService;
            _stateService = stateService;
            _roleService = roleService;
            _paymentModeService = paymentModeService;
            _vehicleCategoryService = vehicleCategoryService;
            _rideCancellationService = rideCancellationService;
            _driverService = driverService;
        }
        
        public IActionResult Index()
        {
            try
            {
                List<string> allStates = _stateService.GetStateNames();
                ViewBag.allStates = allStates;

                return View();
            }
            catch(Exception ex)
            {
                return View(ex.Message);
            }
        }

        public IActionResult Add()
        {
            try
            {
                List<string> roleNames = _roleService.GetRoleNames();

                ViewBag.roleNames = roleNames;

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult Add(UserViewModel user)
        {
            try
            {
                List<string> roleNames = _roleService.GetRoleNames();

                ViewBag.roleNames = roleNames;

                var result = _userService.Add(user);

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.message = "User Registered successfully";
                    return RedirectToAction("Login");
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
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
            var result = _userService.Login(login);

            if (result.IsSuccessStatusCode)
            {
                var token = result.Content.ReadAsStringAsync().Result;
                HttpContext.Session.SetString(sessionToken, token);
                HttpContext.Session.SetString(sessionId, login.UserName);
                return RedirectToAction("BookingRequest");
            }
            TempData["message"] = "Invalid credentials.Please enter the credentials again";
            return View();
        }
        
        public IActionResult BookingRequest()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BookingRequest(BookingViewModel booking)
        {
            var result = await _bookingService.BookingRequest(booking);

            return RedirectToAction("BookingDetails" , result);
           
        }

        public IActionResult BookingDetails(BookingViewModel booking)
        {
            try
            {
                List<string> modeNames = _paymentModeService.GetModeNames();

                ViewBag.modeNames = modeNames;

                List<string> categoryNames = _vehicleCategoryService.GetCategoryNames();

                ViewBag.categoryNames = categoryNames;

                var tuple = new Tuple<BookingViewModel, VehicleCategoryViewModel>
                    (booking, new VehicleCategoryViewModel());

                return View(tuple);
            }
            catch(Exception ex)
            {
                return View(ex.Message);
            }
           
        }

        [HttpPost]
        public IActionResult BookingDetails(int id, BookingViewModel Item1)
        {
            try
            {
                Item1.PaymentMode = Request.Form["PaymentMode"].ToString();
                Item1.VehicleCategory = Request.Form["Type"].ToString();

                List<string> modeNames = _paymentModeService.GetModeNames();

                ViewBag.modeNames = modeNames;

                List<string> categoryNames = _vehicleCategoryService.GetCategoryNames();

                ViewBag.categoryNames = categoryNames;

                if(String.IsNullOrEmpty(Item1.PaymentMode) || String.IsNullOrEmpty(Item1.VehicleCategory))
                {
                    TempData["message"] = "Enter the Required deatils";
                    return View();
                }

                string userName = HttpContext.Session.GetString("username");

                if (Item1.PaymentMode == PaymentModes.Wallet.ToString())
                {
                    UserViewModel user = _userService.GetUserByUserName(userName);
                    if (user.Balance <= (double)Item1.RideFare)
                    {
                        TempData["message"] = "Your wallet balance is Too low , Please add amount to add in the wallet or go back and change the payment mode";
                        return RedirectToAction("AddAmount", user);
                    }
                }

                var result = _bookingService.BookingDetails(Item1, userName);

                if (result.IsSuccessStatusCode)
                {
                    //TempData["message"] = "The booking has been requested successfully";
                    return RedirectToAction("GetRequestedBooking", Item1);
                }

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }

        public IActionResult AddAmount(UserViewModel user)
        {
            return View(user);
        }

        [HttpPost]
        public IActionResult AddAmount(UserViewModel user,int id)
        {
            string userName = HttpContext.Session.GetString("username");
            var result = _userService.Update(userName,user);

            if (result.IsSuccessStatusCode)
            {
                TempData["message"] = "Amount has been Added successfully";
                return RedirectToAction("BookingRequest");
            }

            TempData["message"] = "Amount could not be Added successfully";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetRequestedBooking(BookingViewModel booking)
        {
            try
            {
                string userName = HttpContext.Session.GetString("drivername");
               
                int bookingId = _bookingService.GetBookingId(booking);

                booking = _bookingService.GetBookingById(bookingId);

                bool isAssigned =  await _driverService.GetDriver(booking , userName);

                if(isAssigned)
                {
                    TempData["message"] = "The booking has been confirmed successfully";
                    return RedirectToAction("ConfirmedBooking", booking);
                }
                else
                {
                    TempData["message"] = "There are no available drivers in your area";
                    return View(booking);
                }
                /* if (booking.Status == Status.Booked.ToString())
                 {
                     TempData["message"] = "The booking has been confirmed successfully";
                     return RedirectToAction("ConfirmedBooking", booking);

                 }*/
                
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CancelRequest(BookingViewModel booking)
        {
            try
            {
                string userName = HttpContext.Session.GetString("username");
                int bookingId = _bookingService.GetBookingId(booking);

                var result = _bookingService.CancelRequest(bookingId, userName);

                if (result.IsSuccessStatusCode)
                {
                    TempData["message"] = "Booking has been cancelled successfully";
                    return RedirectToAction("BookingRequest");
                }

                TempData["message"] = "Booking cannot be cancelled due to some system issue";

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }    
        

        [HttpGet]
        public IActionResult ConfirmedBooking(BookingViewModel booking)
        {
            try
            {
               // Thread.Sleep(20000);
                DriverViewModel driver = _driverService.GetDriverById((int)booking.DriverId);

                driver.OTP = (new Random()).Next(100, 1000).ToString();

                HttpContext.Session.SetString(otp, driver.OTP);

                BookingViewModel bookingViewModel = _bookingService.GetBookingById(booking.Id);

                if (bookingViewModel.Status == Status.Started.ToString())
                {
                    HttpContext.Session.Remove("otp");
                    TempData["message"] = "Ride has been started successfully";
                    return RedirectToAction("RideStarted", booking);
                }


                return View(driver);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult RideStarted(BookingViewModel booking)
        {
            try
            {
               // Thread.Sleep(20000);
                DriverViewModel driver = _driverService.GetDriverById((int)booking.DriverId);

                BookingViewModel bookingViewModel = _bookingService.GetBookingById(booking.Id);

                if (bookingViewModel.Status == Status.Completed.ToString())
                    return RedirectToAction("BookingRequest");

                return View(driver);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }


        [HttpGet]
        public IActionResult ConfirmedBookingCancellation(CancellationReasonsViewModel cancellationReason)
        {
            try
            {
                List<string> reasonNames = _userService.GetReasonNames();
                ViewBag.reasonNames = reasonNames;

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }


        [HttpPost]
        public IActionResult ConfirmedBookingCancellation(RideCancellationViewModel rideCancellation)
        {
            try
            {
                List<string> reasonNames = _userService.GetReasonNames();
                ViewBag.reasonNames = reasonNames;

                string userName = HttpContext.Session.GetString("username");

                var result = _bookingService.Add(rideCancellation, userName);

                if (result.IsSuccessStatusCode)
                {
                    TempData["message"] = "Booking has been cancelled successfully";
                    return RedirectToAction("BookingRequest");
                }

                TempData["message"] = "Booking cannot be cancelled due to some system issue";

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}
