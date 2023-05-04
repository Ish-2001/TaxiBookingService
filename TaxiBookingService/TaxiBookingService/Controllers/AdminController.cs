using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Logging;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Controllers
{
    [ApiController]
    [Route("Taxi-Booking-Service/admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IJwtService _jwtService;
        private readonly ILog _logger;

        public AdminController(IAdminService adminService, ILog logger, IJwtService jwtService)
        {
            _logger = logger;
            _adminService = adminService;
            _jwtService = jwtService;
        }


        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]LoginDTO login)
        {
            try
            {
                if (_adminService.Login(login))
                {
                    //string token = _jwtService.GenerateToken(login.UserName, login.Password);
                    var token = _jwtService.GenerateToken(login.UserName, login.Password);
                    var message = Ok(token);
                    _logger.Information("User has been logged in Successfully");
                    return message;
                }
                else
                {
                    var message = Unauthorized("User is not present");
                    _logger.Warning("User is not present");
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
        [Route("approveDriver/{userName}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Login(string userName)
        {
            try
            {
                if (_adminService.ApproveDriver(userName))
                {
                    var message = Ok("Driver has been approved successfully");
                    _logger.Information("Diver has been approved in Successfully");
                    return message;
                }
                else
                {
                    var message = Unauthorized("Driver cannot be approved successfully");
                    _logger.Warning("Driver cannot be approved successfully");
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
