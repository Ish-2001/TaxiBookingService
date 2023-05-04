using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Logging;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Controllers
{
    [ApiController]
    [Route("Taxi-Booking-Service/drivercomplaint")]
    public class DriverComplaintController : Controller
    {
        private readonly IDriverComplaintService _complaintService;
        private readonly ILog _logger;

        public DriverComplaintController(IDriverComplaintService complaintService, ILog logger)
        {
            _logger = logger;
            _complaintService = complaintService;
        }

        [Route("getall")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var message = Ok(_complaintService.GetAll());
                _logger.Information("Displayed the list of complaints successfully");
                return message;
            }
            catch (Exception ex)
            {
                var message = Problem(ex.Message);
                _logger.Error(ex.StackTrace);
                return message;
            }

        }

        [Route("getcomplaints")]
        [HttpGet]
        public IActionResult GetDriverComplaints()
        {
            try
            {
                var message = Ok(_complaintService.GetDriverComplaints());
                _logger.Information("Displayed the list of complaints successfully");
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
        public IActionResult Add(DriverComplaintDTO newComplaint)
        {
            try
            {
                if (_complaintService.Add(newComplaint))
                {
                    var message = Ok("Complaint has been added Successfully");
                    _logger.Information("Complaint has been added Successfully");
                    return message;
                }
                else
                {
                    var message = BadRequest("Complaint is already present");
                    _logger.Warning("Complaint is already present");
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
