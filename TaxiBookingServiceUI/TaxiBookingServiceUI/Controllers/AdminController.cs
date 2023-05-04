using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaxiBookingServiceUI.Models;
using TaxiBookingServiceUI.Services;
using static GoogleMaps.LocationServices.Directions;

namespace TaxiBookingServiceUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;
        private readonly DriverService _driverService;
        private readonly IHttpContextAccessor _contextAccessor;
        public const string sessionToken = "admintoken";
        public const string sessionId = "username";

        public AdminController(IHttpContextAccessor httpContextAccessor, AdminService adminService, DriverService driverService)
        {
            _adminService = adminService;
            _contextAccessor = httpContextAccessor;
            _driverService = driverService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            HttpContext.Session.Remove("admintoken");
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            var result = _adminService.Login(login);

            if (result.IsSuccessStatusCode)
            {
                var token = result.Content.ReadAsStringAsync().Result;
                HttpContext.Session.SetString(sessionToken, token);
                HttpContext.Session.SetString(sessionId, login.UserName);
                return RedirectToAction("Index");
            }
            TempData["message"] = "Invalid credentials.Please enter the credentials again";
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> GetComplaints()
        {
           
            return View(await _driverService.GetComplaints());
        }

    }
        
}
