using Microsoft.AspNetCore.Mvc;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Logging;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Controllers
{
    [ApiController]
    [Route("Taxi-Booking-Service/ridecancellation")]
    public class RideCancellationController : Controller
    {
        private readonly IRideCancellationService _rideCancellationService;
        private readonly ILog _logger;

        public RideCancellationController(IRideCancellationService rideCancellationService, ILog logger)
        {
            _logger = logger;
            _rideCancellationService = rideCancellationService;
        }

        [Route("getridecancellations")]
        [HttpGet]
        public IActionResult GetPaymentModes()
        {
            try
            {
                var message = Ok(_rideCancellationService.GetPaymentModes());
                _logger.Information("Displayed the list of ride cancellations successfully");
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
                var message = Ok(_rideCancellationService.GetAll());
                _logger.Information("Displayed the list of ride cancellations successfully");
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
        public IActionResult Add(RideCancellationDTO rideCancellationDTO)
        {
            try
            {
                if (_rideCancellationService.Add(rideCancellationDTO))
                {
                    var message = Ok("Ride cancellation added successfully");
                    _logger.Information("Ride cancellation has been added Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Ride cancellation is already present");
                    _logger.Warning("Ride cancellation is already present");
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
