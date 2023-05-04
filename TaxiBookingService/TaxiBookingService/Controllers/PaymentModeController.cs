using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Logging;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Controllers
{
    [ApiController]
    [Route("Taxi-Booking-Service/paymentmode")]
    public class PaymentModeController : Controller
    {
        private readonly IPaymentModeService _modeService;
        private readonly ILog _logger;

        public PaymentModeController(IPaymentModeService detailService, ILog logger)
        {
            _logger = logger;
            _modeService = detailService;
        }

        [Route("getpaymentmodes")]
        [HttpGet]
        
        public IActionResult GetPaymentModes()
        {
            try
            {
                var message = Ok(_modeService.GetPaymentModes());
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
                var message = Ok(_modeService.GetAll());
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
        [Authorize(Roles = "Admin")]
        public IActionResult Add(PaymentModeDTO newDetail)
        {
            try
            {
                if (_modeService.Add(newDetail))
                {
                    var message = Ok("Payment mode has been added Successfully");
                    _logger.Information("Payment mode has been added Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Payment mode is already present");
                    _logger.Warning("Payment mode is already present");
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
        public IActionResult Update(PaymentModeDTO updatedDetail, int id)
        {
            try
            {
                if (_modeService.Update(updatedDetail, id))
                {
                    var message = Ok("Payment mode has been updated Successfully");
                    _logger.Information("Payment mode has been updated Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Payment mode to be updated is not present");
                    _logger.Warning("Payment mode to be updated is not present");
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
                if (_modeService.Delete(id))
                {
                    var message = Ok("Payment mode has been deleted Successfully");
                    _logger.Information("Payment mode has been deleted Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Payment mode to be deleted is not present");
                    _logger.Warning("Payment mode to be deleted is not present");
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
