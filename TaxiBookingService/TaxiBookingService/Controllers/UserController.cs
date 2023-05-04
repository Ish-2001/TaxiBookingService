using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Logging;
using TaxiBookingService.Services.Interfaces;
using TaxiBookingService.Services.Service;
using Formatting = Newtonsoft.Json.Formatting;

namespace TaxiBookingService.Controllers
{
    [Route("Taxi-Booking-Service/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBookingService _bookingService;
        private readonly IJwtService _jwtService;
        private readonly ILog _logger;

        public UserController(IUserService userService, ILog logger, IJwtService jwtService , IBookingService bookingService)
        {
            _logger = logger;
            _userService = userService;
            _jwtService = jwtService;
            _bookingService = bookingService;
        }

      
        [Route("getusers")]
        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var message = Ok(_userService.GetUsers());
                _logger.Information("Displayed the list of users successfully");
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
                var message = Ok(_userService.GetAll());
                _logger.Information("Displayed the list of users successfully");
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
       
        public IActionResult Add(UserDTO newUser)
        {
            try
            {
                if (_userService.Add(newUser))
                {
                    var message = Ok("User has been registered Successfully");
                    _logger.Information("User has been registered Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("User is already present");
                    _logger.Warning("User is already present");
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

        [Route("update/{id}")]
        [HttpPut]
      
        public IActionResult Update(UpdateUserDTO updatedUser , int id)
        {
            try
            {
                if (_userService.Update(updatedUser,id))
                {
                    var message = Ok("User has been updated Successfully");
                    _logger.Information("User has been updated Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("User to be updated is not present");
                    _logger.Warning("User to be updated is not present");
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

        [Route("delete/{id}")]
        [HttpDelete]
       
        public IActionResult Delete(int id)
        {
            try
            {
                if (_userService.Delete(id))
                {
                    var message = Ok("User has been deleted Successfully");
                    _logger.Information("User has been deleted Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("User to be deleted is not present");
                    _logger.Warning("User to be deleted is not present");
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
                if (_userService.Login(login))
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


        [HttpGet]
        [Route("bookinghistory/{userName}")]
        public IActionResult GetBookingHistory(string userName)
        {
            try
            {
                var message = Ok(_bookingService.GetBookingHistory(userName));
                _logger.Information("Displayed the list of bookings successfully");
                return message;
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
