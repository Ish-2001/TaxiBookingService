using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Logging;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Controllers
{

    [ApiController]
    [Route("Taxi-Booking-Service/bookingstatus")]
    public class BookingStatusController : Controller
    {
        private readonly IBookingStatusService _statusService;
        private readonly ILog _logger;

        public BookingStatusController(IBookingStatusService statusService, ILog logger)
        {
            _logger = logger;
            _statusService = statusService;
        }

        [Route("getbookingstatus")]
        [HttpGet]
        public IActionResult GetBookingStatus()
        {
            try
            {
                var message = Ok(_statusService.GetBookingStatus());
                _logger.Information("Displayed the list of statuses successfully");
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
                var message = Ok(_statusService.GetAll());
                _logger.Information("Displayed the list of statuses successfully");
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
        public IActionResult Add(BookingStatusDTO newState)
        {
            try
            {
                if (_statusService.Add(newState))
                {
                    var message = Ok("Status has been added Successfully");
                    _logger.Information("Status has been added Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Status is already present");
                    _logger.Warning("Status is already present");
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
        public IActionResult Update(BookingStatusDTO updatedState, int id)
        {
            try
            {
                if (_statusService.Update(updatedState, id))
                {
                    var message = Ok("Status has been updated Successfully");
                    _logger.Information("Status has been updated Successfully");
                    string json = JsonConvert.SerializeObject(Formatting.Indented, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    return message;
                }
                else
                {
                    var message = BadRequest("Status to be updated is not present");
                    _logger.Warning("Status to be updated is not present");
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
                if (_statusService.Delete(id))
                {
                    var message = Ok("Status has been deleted Successfully");
                    _logger.Information("Status has been deleted Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Status to be deleted is not present");
                    _logger.Warning("Status to be deleted is not present");
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
