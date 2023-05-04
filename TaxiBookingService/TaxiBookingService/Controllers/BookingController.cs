using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Logging;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Controllers
{
    [Route("Taxi-Booking-Service/booking")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IPaymentService _paymentService;
        private readonly ILog _logger;

        public BookingController(IBookingService bookingService, ILog logger, IPaymentService paymentService)
        {
            _logger = logger;
            _bookingService = bookingService;
            _paymentService = paymentService;
        }


        [Route("getbookings")]
        [HttpGet]
        public IActionResult GetBookings()
        {
            try
            {
                var message = Ok(_bookingService.GetBookings());
                _logger.Information("Displayed the list of bookings successfully");
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
                var message = Ok(_bookingService.GetAll());
                _logger.Information("Displayed the list of bookings successfully");
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
       
        public IActionResult Add(BookingDTO newBooking)
        {
            try
            {
                if (_bookingService.Add(newBooking))
                {
                    var message = Ok("Booking has been added Successfully");
                    _logger.Information("Booking has been added Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Booking is already present");
                    _logger.Warning("Booking is already present");
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
        public IActionResult Update(int id,BookingDTO updatedBooking)
        {
            try
            {
                if (_bookingService.Update(updatedBooking, id))
                {
                    var message = Ok("Booking has been updated Successfully");
                    _logger.Information("Booking has been updated Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Booking to be updated is not present");
                    _logger.Warning("Booking to be updated is not present");
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

        [Route("getpayments")]
        [HttpGet]
        public IActionResult GetPayments()
        {
            try
            {
                var message = Ok(_paymentService.GetAll());
                _logger.Information("Displayed the list of payments successfully");
                return message;
            }
            catch (Exception ex)
            {
                var message = Problem(ex.Message);
                _logger.Error(ex.StackTrace);
                return message;
            }
        }


        [Route("postrequest/{bookingId}/{userName}")]
        [HttpPut]
        public IActionResult PostRequest(int bookingId, string userName)
        {
            try
            {
                if (_bookingService.PostRequest(bookingId,userName))
                {
                    var message = Ok("Post request sent Successfully");
                    _logger.Information("Post request sent Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Post request could not be sent Successfully");
                    _logger.Warning("Post request could not be sent Successfully");
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
