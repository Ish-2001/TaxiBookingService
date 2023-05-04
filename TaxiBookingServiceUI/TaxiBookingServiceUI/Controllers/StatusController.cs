using Microsoft.AspNetCore.Mvc;
using TaxiBookingServiceUI.Services;

namespace TaxiBookingServiceUI.Controllers
{
    public class StatusController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly StatusService _statusService;

        public StatusController(IHttpContextAccessor httpContextAccessor, StatusService statusService)
        {
            _contextAccessor = httpContextAccessor;
            _statusService = statusService;
        }

        public async Task<IActionResult> GetAll()
        {
            return View(await _statusService.GetAll());
        }
    }
}
