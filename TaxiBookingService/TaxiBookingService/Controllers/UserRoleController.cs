using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Logging;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Controllers
{
    [ApiController]
    [Route("Taxi-Booking-Service/role")]
    public class UserRoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly ILog _logger;

        public UserRoleController(IRoleService roleService, ILog logger)
        {
            _logger = logger;
            _roleService = roleService;
        }

        [Route("getuserroles")]
        [HttpGet]
        public IActionResult GetUserRoles()
        {
            try
            {
                var message = Ok(_roleService.GetRoles());
                _logger.Information("Displayed the list of user roles successfully");
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
                var message = Ok(_roleService.GetAll());
                _logger.Information("Displayed the list of user roles successfully");
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
      
        public IActionResult Add(UserRoleDTO newRole)
        {
            try
            {
                if (_roleService.Add(newRole))
                {
                    var message = Ok("User role been added Successfully");
                    _logger.Information("User role been added Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("User role is already present");
                    _logger.Warning("User role is already present");
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
      
        public IActionResult Update(UserRoleDTO updatedRole, int id)
        {
            try
            {
                if (_roleService.Update(updatedRole, id))
                {
                    var message = Ok("User role has been updated Successfully");
                    _logger.Information("User role has been updated Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("User role to be updated is not present");
                    _logger.Warning("User role to be updated is not present");
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
                if (_roleService.Delete(id))
                {
                    var message = Ok("User role has been deleted Successfully");
                    _logger.Information("User role has been deleted Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("User role to be deleted is not present");
                    _logger.Warning("User role  to be deleted is not present");
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
