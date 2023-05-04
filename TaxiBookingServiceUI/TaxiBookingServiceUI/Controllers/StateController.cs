using Microsoft.AspNetCore.Mvc;
using TaxiBookingServiceUI.Models;
using TaxiBookingServiceUI.Services;

namespace TaxiBookingServiceUI.Controllers
{
    public class StateController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly StateService _stateService;

        public StateController(IHttpContextAccessor httpContextAccessor, StateService stateService)
        {
            _contextAccessor = httpContextAccessor;
            _stateService = stateService;
        }

        public async Task<IActionResult> GetAll()
        {
            return View(await _stateService.GetAll());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(StateViewModel state)
        {
            string userName = HttpContext.Session.GetString("username");

            var result = _stateService.Add(state, userName);

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
                _stateService.Delete(id);
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }

            return RedirectToAction("GetAll");
        }

        public IActionResult Edit(StateViewModel city)
        {
            return View(city);
        }

        [HttpPost]
        public IActionResult Edit(StateViewModel state, int id)
        {
            var result = _stateService.Edit(state, id);

            if (result.IsSuccessStatusCode)
            {
                TempData["message"] = "The book has been updated successfully";
                return RedirectToAction("GetAll");
            }

            TempData["message"] = "The book cannot be updated";
            return View();
        }
    }
}
