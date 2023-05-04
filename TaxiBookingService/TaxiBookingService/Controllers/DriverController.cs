using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Logging;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Controllers
{
    [Route("Taxi-Booking-Service/driver")]
    [ApiController]
    public class DriverController : Controller
    {
        private readonly IDriverService _driverService;
        private readonly IJwtService _jwtService;
        private readonly ILog _logger;

        public DriverController(IDriverService driverService, ILog logger, IJwtService jwtService)
        {
            _logger = logger;
            _driverService = driverService;
            _jwtService = jwtService;
        }

        [Route("getdrivers")]
        [HttpGet]
        public IActionResult Getdrivers()
        {
            try
            {
                var message = Ok(_driverService.GetDrivers());
                _logger.Information("Displayed the list of drivers successfully");
                return message;
            }
            catch (Exception ex)
            {
                var message = Problem(ex.Message);
                _logger.Error(ex.StackTrace);
                return message;
            }

        }

        [Route("getall")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var message = Ok(_driverService.GetAll());
                _logger.Information("Displayed the list of drivers successfully");
                return message;
            }
            catch (Exception ex)
            {
                var message = Problem(ex.Message);
                _logger.Error(ex.StackTrace);
                return message;
            }

        }

        [Route("add")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(DriverDTO newDriver)
        {
            try
            {
                if (_driverService.Add(newDriver))
                {
                    var message = Ok("Driver has been added Successfully");
                    _logger.Information("Driver has been added Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Driver is already present");
                    _logger.Warning("Driver is already present");
                    return message;
                }
            }
            catch (Exception ex)
            {
                var message = Problem(ex.Message);
                _logger.Error(ex.StackTrace);
                return message;
            }

            
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginDTO login)
        {
            try
            {
                if (_driverService.Login(login))
                {
                    string token = _jwtService.GenerateToken(login.UserName, login.Password);
                    var message = Ok(token);
                    _logger.Information("Driver has been logged in Successfully");
                    return message;
                }
                else
                {
                    var message = Unauthorized("Driver is not present");
                    _logger.Warning("Driver is not present");
                    return message;
                }
            }
            catch (Exception ex)
            {
                var message = Problem(ex.Message);
                _logger.Error(ex.StackTrace);
                return message;
            }
        }

        [HttpPut]
        [Route("completepayment/{id}")]
        [Authorize(Roles = "Driver")]
        public IActionResult CompletePayment(int id, PaymentDTO updatedPayment)
        {
            try
            {
                if (_driverService.CompletePayment(id, updatedPayment))
                {
                    var message = Ok("Payment has been completed successfully");
                    _logger.Information("Payment has been completed successfully");
                    return message;
                }
                else
                {
                    var message = Unauthorized("Payment could not be completed");
                    _logger.Warning("Payment could not be completed");
                    return message;
                }
            }
            catch (Exception ex)
            {
                var message = Problem(ex.Message);
                _logger.Error(ex.StackTrace);
                return message;
            }
        }
    }
}
