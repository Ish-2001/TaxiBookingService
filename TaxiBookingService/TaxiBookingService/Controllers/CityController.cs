using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Logging;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Controllers
{
    [Route("Taxi-Booking-Service/city")]
    [ApiController]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private readonly ILog _logger;

        public CityController(ICityService cityService, ILog logger)
        {
            _logger = logger;
            _cityService = cityService;
        }

        [Route("getcities")]
        [HttpGet]
      
        public IActionResult GetCities()
        {
            try
            {
                var message = Ok(_cityService.GetCities());
                _logger.Information("Displayed the list of cities successfully");
                return message;
            }
            catch (Exception ex)
            {
                var message = Problem(ex.StackTrace);
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
                var message = Ok(_cityService.GetAll());
                _logger.Information("Displayed the list of cities successfully");
                return message;
            }
            catch (Exception ex)
            {
                var message = Problem(ex.StackTrace);
                _logger.Error(ex.StackTrace);
                return message;
            }

        }

        [Route("add")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(CityDTO newCity)
        {
            try
            {
                if (_cityService.Add(newCity))
                {
                    var message = Ok("City has been added Successfully");
                    _logger.Information("City has been added Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("City is already present");
                    _logger.Warning("City is already present");
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
        public IActionResult Update(CityDTO updatedCity, int id)
        {
            try
            {
                if (_cityService.Update(updatedCity, id))
                {
                    var message = Ok("City has been updated Successfully");
                    _logger.Information("City has been updated Successfully");
                    string json = JsonConvert.SerializeObject(Formatting.Indented, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    return message;
                }
                else
                {
                    var message = BadRequest("City to be updated is not present");
                    _logger.Warning("City to be updated is not present");
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
                if (_cityService.Delete(id))
                {
                    var message = Ok("City has been deleted Successfully");
                    _logger.Information("City has been deleted Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("City to be deleted is not present");
                    _logger.Warning("City to be deleted is not present");
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
