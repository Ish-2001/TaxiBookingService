using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Logging;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Controllers
{
    [Route("Taxi-Booking-Service/state")]
    [ApiController]
    public class StateController : Controller
    {
        private readonly IStateService _stateService;
        private readonly ILog _logger;

        public StateController(IStateService stateService, ILog logger)
        {
            _logger = logger;
            _stateService = stateService;
         
        }

        [Route("getstates")]
        [HttpGet]
        public IActionResult GetStates()
        {
            try
            {
                var message = Ok(_stateService.GetStates());
                _logger.Information("Displayed the list of states successfully");
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
                var message = Ok(_stateService.GetAll());
                _logger.Information("Displayed the list of states successfully");
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
        public IActionResult Add(StateDTO newState)
        {
            try
            {
                if (_stateService.Add(newState))
                {
                    var message = Ok("State has been added Successfully");
                    _logger.Information("State has been added Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("State is already present");
                    _logger.Warning("State is already present");
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
        [Authorize(Roles = "Admin")]
        public IActionResult Update(StateDTO updatedState, int id)
        {
            try
            {
                if (_stateService.Update(updatedState, id))
                {
                    var message = Ok("State has been updated Successfully");
                    _logger.Information("State has been updated Successfully");
                    
                    return message;
                }
                else
                {
                    var message = BadRequest("State to be updated is not present");
                    _logger.Warning("State to be updated is not present");
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
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_stateService.Delete(id))
                {
                    var message = Ok("State has been deleted Successfully");
                    _logger.Information("State has been deleted Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("State to be deleted is not present");
                    _logger.Warning("State to be deleted is not present");
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
