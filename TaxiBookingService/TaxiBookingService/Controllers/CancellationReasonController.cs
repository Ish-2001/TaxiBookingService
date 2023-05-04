using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Logging;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Controllers
{
    [ApiController]
    [Route("Taxi-Booking-Service/cancellationreason")]
    public class CancellationReasonController : Controller
    {
        private readonly ICancellationReasonService _reasonService;
        private readonly ILog _logger;

        public CancellationReasonController(ICancellationReasonService reasonService, ILog logger)
        {
            _logger = logger;
            _reasonService = reasonService;
        }

        [Route("getcancellationreasons")]
        [HttpGet]
        public IActionResult GetCancellationReasons()
        {
            try
            {
                var message = Ok(_reasonService.GetCancellationReasons());
                _logger.Information("Displayed the list of reasons successfully");
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
                var message = Ok(_reasonService.GetAll());
                _logger.Information("Displayed the list of reasons successfully");
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
        public IActionResult Add(CancellationReasonDTO newReason)
        {
            try
            {
                if (_reasonService.Add(newReason))
                {
                    var message = Ok("Reason has been added Successfully");
                    _logger.Information("Reason has been added Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Reason is already present");
                    _logger.Warning("Reason is already present");
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
        public IActionResult Update(CancellationReasonDTO updatedReason, int id)
        {
            try
            {
                if (_reasonService.Update(updatedReason, id))
                {
                    var message = Ok("Reason has been updated Successfully");
                    _logger.Information("Reason has been updated Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Reason to be updated is not present");
                    _logger.Warning("Reason to be updated is not present");
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
                if (_reasonService.Delete(id))
                {
                    var message = Ok("Reason has been deleted Successfully");
                    _logger.Information("Reason has been deleted Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Reason to be deleted is not present");
                    _logger.Warning("Reason to be deleted is not present");
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
