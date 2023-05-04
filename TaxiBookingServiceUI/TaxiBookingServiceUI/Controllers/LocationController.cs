using Microsoft.AspNetCore.Mvc;
using TaxiBookingServiceUI.Models;
using TaxiBookingServiceUI.Services;

namespace TaxiBookingServiceUI.Controllers
{
    public class LocationController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly LocationService _locationService;

        public LocationController(IHttpContextAccessor httpContextAccessor, LocationService stateService)
        {
            _contextAccessor = httpContextAccessor;
            _locationService = stateService;
        }

        public async Task<IActionResult> GetAll()
        {
            return View(await _locationService.GetAll());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(LocationViewModel state)
        {
            string userName = HttpContext.Session.GetString("username");

            var result = _locationService.Add(state, userName);

            if (result.IsSuccessStatusCode)
            {
                TempData["message"] = "Book has been added successfully";
                return RedirectToAction("GetAll");
            }

            TempData["message"] = "Book with this serial number is already present in the system";

            return View();
        }

        public IActionResult Delete(int id)
        {
            try
            {
                TempData["message"] = "The book has been deleted successfully";
                _locationService.Delete(id);
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }

            return RedirectToAction("GetAll");
        }

        /*public IActionResult Edit(LocationViewModel location)
        {
            return View(location);
        }

        [HttpPost]
        public IActionResult Edit(LocationViewModel state, int id)
        {
            var result = _locationService.Edit(state, id);

            if (result.IsSuccessStatusCode)
            {
                TempData["message"] = "The book has been updated successfully";
                return RedirectToAction("GetAll");
            }

            TempData["message"] = "The book cannot be updated";
            return View();
        }*/
    }
}
