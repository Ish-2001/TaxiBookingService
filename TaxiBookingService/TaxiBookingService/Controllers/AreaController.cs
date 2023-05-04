using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Logging;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Controllers
{
    [ApiController]
    [Route("Taxi-Booking-Service/area")]
    public class AreaController : Controller
    {
        private readonly IAreaService _areaService;
        private readonly ILog _logger;

        public AreaController(IAreaService areaService, ILog logger)
        {
            _logger = logger;
            _areaService = areaService;
        }

        [Route("getall")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var message = Ok(_areaService.GetAll());
                _logger.Information("Displayed the list of areas successfully");
                return message;
            }
            catch (Exception ex)
            {
                var message = Problem(ex.Message);
                _logger.Error(ex.StackTrace);
                return message;
            }

        }

        [Route("getareas")]
        [HttpGet]
        public IActionResult GetAreas()
        {
            try
            {
                var message = Ok(_areaService.GetAreas());
                _logger.Information("Displayed the list of areas successfully");
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
        public IActionResult Add([FromBody]AreaDTO newArea)
        {
            try
            {
                if (_areaService.Add(newArea))
                {
                    var message = Ok("Area has been added Successfully");
                    _logger.Information("Area has been added Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Area is already present");
                    _logger.Warning("Area is already present");
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
        public IActionResult Update(AreaDTO updatedArea, int id)
        {
            try
            {
                if (_areaService.Update(updatedArea, id))
                {
                    var message = Ok("Area has been updated Successfully");
                    _logger.Information("Area has been updated Successfully");
                    
                    return message;
                }
                else
                {
                    var message = BadRequest("Area to be updated is not present");
                    _logger.Warning("Area to be updated is not present");
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
                if (_areaService.Delete(id))
                {
                    var message = Ok("Area has been deleted Successfully");
                    _logger.Information("Area has been deleted Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Area to be deleted is not present");
                    _logger.Warning("Area to be deleted is not present");
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
