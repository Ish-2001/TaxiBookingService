using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Web.Http.Results;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Logging;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Controllers
{
    [ApiController]
    [Route("Taxi-Booking-Service/driverRating")]
    public class DriverRatingController : Controller
    {
        private readonly IDriverRatingService _driverRatingService;
        private readonly ILog _logger;

        public DriverRatingController(IDriverRatingService driverRatingService, ILog logger)
        {
            _logger = logger;
            _driverRatingService = driverRatingService;
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Add(DriverRatingDTO driverRating)
        {
            try
            {
                if (_driverRatingService.Add(driverRating))
                {
                    var message = Ok("Rating has been added Successfully");
                    _logger.Information("Rating has been added Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Rating is already present");
                    _logger.Warning("Rating is already present");
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
