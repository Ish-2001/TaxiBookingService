using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Logging;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Controllers
{
    [Route("Taxi-Booking-Service/vehiclecategory")]
    [ApiController]
    public class VehicleCategoryController : Controller
    {
        private readonly IVehicleCategoryService _vehicleCategoryService;
        private readonly ILog _logger;

        public VehicleCategoryController(IVehicleCategoryService vehicleCategoryService, ILog logger)
        {
            _logger = logger;
            _vehicleCategoryService = vehicleCategoryService;
        }

        [Route("getvecilecategories")]
        [HttpGet]
        
        public IActionResult GetVehicleCategories()
        {
            try
            {
                var message = Ok(_vehicleCategoryService.GetVehicleCategories());
                _logger.Information("Displayed the list of vehicle categories successfully");
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
                var message = Ok(_vehicleCategoryService.GetAll());
                _logger.Information("Displayed the list of vehicle categories successfully");
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
        public IActionResult Add(VehicleCategoryDTO newCity)
        {
            try
            {
                if (_vehicleCategoryService.Add(newCity))
                {
                    var message = Ok("Vehicle category has been added Successfully");
                    _logger.Information("Vehicle category has been added Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Vehicle category is already present");
                    _logger.Warning("Vehicle category is already present");
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
        public IActionResult Update(VehicleCategoryDTO updatedCity, int id)
        {
            try
            {
                if (_vehicleCategoryService.Update(updatedCity, id))
                {
                    var message = Ok("Vehicle category has been updated Successfully");
                    _logger.Information("Vehicle category has been updated Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Vehicle category to be updated is not present");
                    _logger.Warning("Vehicle category to be updated is not present");
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
                if (_vehicleCategoryService.Delete(id))
                {
                    var message = Ok("Vehicle category has been deleted Successfully");
                    _logger.Information("Vehicle category has been deleted Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Vehicle category to be deleted is not present");
                    _logger.Warning("Vehicle category to be deleted is not present");
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
