using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Logging;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Controllers
{
    [Route("Taxi-Booking-Service/vehicledetails")]
    [ApiController]
    public class VehicleDetailsController : Controller
    {
        private readonly IVehicleDetailsService _detailService;
        private readonly ILog _logger;

        public VehicleDetailsController(IVehicleDetailsService detailService, ILog logger)
        {
            _logger = logger;
            _detailService = detailService;
        }

        [Route("getvehicledetails")]
        [HttpGet]
        public IActionResult GetVehicleDetails()
        {
            try
            {
                var message = Ok(_detailService.GetVehicleDetails());
                _logger.Information("Displayed the list of vehicle details successfully");
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
                var message = Ok(_detailService.GetAll());
                _logger.Information("Displayed the list of vehicle details successfully");
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
        [Authorize(Roles = "Driver")]
        public IActionResult Add(VehicleDetailsDTO newDetail)
        {
            try
            {
                if (_detailService.Add(newDetail))
                {
                    var message = Ok("Vehicle detail has been added Successfully");
                    _logger.Information("Vehicle detail has been added Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Vehicle detail is already present");
                    _logger.Warning("Vehicle detail is already present");
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
       
        public IActionResult Update(VehicleDetailsDTO updatedDetail, int id)
        {
            try
            {
                if (_detailService.Update(updatedDetail, id))
                {
                    var message = Ok("Vehicle detail has been updated Successfully");
                    _logger.Information("Vehicle detail has been updated Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Vehicle detail to be updated is not present");
                    _logger.Warning("Vehicle detail to be updated is not present");
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
                if (_detailService.Delete(id))
                {
                    var message = Ok("Vehicle detail has been deleted Successfully");
                    _logger.Information("Vehicle detail has been deleted Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Vehicle detail to be deleted is not present");
                    _logger.Warning("Vehicle detail to be deleted is not present");
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
