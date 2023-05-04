using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Logging;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Controllers
{
    [Route("Taxi-Booking-Service/location")]
    [ApiController]
    public class LocationController : Controller
    {

        private readonly ILocationService _locationService;
        private readonly ILog _logger;

        public LocationController(ILocationService locationService, ILog logger)
        {
            _logger = logger;
            _locationService = locationService;
        }

        [Route("getlocations")]
        [HttpGet]
        public IActionResult GetLocations()
        {
            try
            {
                var message = Ok(_locationService.GetLocations());
                _logger.Information("Displayed the list of locations successfully");
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
                var message = Ok(_locationService.GetAll());
                _logger.Information("Displayed the list of locations successfully");
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
        //[Authorize(Roles = "Driver")]
        public IActionResult Add(LocationDTO newLocation)
        {
            try
            {
                if (_locationService.Add(newLocation))
                {
                    var message = Ok("Location has been added Successfully");
                    _logger.Information("Location has been added Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Location is already present");
                    _logger.Warning("Location is already present");
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
        [Authorize(Roles = "Driver")]
        public IActionResult Update(LocationDTO updatedLocation, int id)
        {
            try
            {
                if (_locationService.Update(updatedLocation, id))
                {
                    var message = Ok("Location has been updated Successfully");
                    _logger.Information("Location has been updated Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Location to be updated is not present");
                    _logger.Warning("Location to be updated is not present");
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
                if (_locationService.Delete(id))
                {
                    var message = Ok("Location has been deleted Successfully");
                    _logger.Information("Location has been deleted Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Location to be deleted is not present");
                    _logger.Warning("Location to be deleted is not present");
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
