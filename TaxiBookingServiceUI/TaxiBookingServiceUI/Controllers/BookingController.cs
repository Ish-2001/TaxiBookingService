using Microsoft.AspNetCore.Mvc;

namespace TaxiBookingServiceUI.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
