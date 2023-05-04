using Microsoft.AspNetCore.Mvc;

namespace TaxiBookingService.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
