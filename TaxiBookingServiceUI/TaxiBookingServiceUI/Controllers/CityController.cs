using Microsoft.AspNetCore.Mvc;
using TaxiBookingServiceUI.Models;
using TaxiBookingServiceUI.Services;

namespace TaxiBookingServiceUI.Controllers
{
    public class CityController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly CityService _cityService;
        private readonly StateService _stateService;

        public CityController(IHttpContextAccessor httpContextAccessor, CityService cityService , StateService stateService)
        {
            _contextAccessor = httpContextAccessor;
            _cityService = cityService;
            _stateService = stateService;
        }

        public async Task<IActionResult> GetAll()
        {
            try
            {
                return View(await _cityService.GetAll());
            }
            catch(Exception ex)
            {
                return View(ex.Message);
            }
            
        }

        [HttpGet]
        public IActionResult GetCities(string filter)
        {
            try
            {
                return Json(_cityService.GetCities(filter));
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult Add()
        {
            try
            {
                List<string> allStates = _stateService.GetStateNames();
                ViewBag.allStates = allStates;
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Add(CityViewModel city)
        {
            try
            {
                List<string> allStates = _stateService.GetStateNames();
                ViewBag.allStates = allStates;

                string userName = HttpContext.Session.GetString("username");

                var result = _cityService.Add(city, userName);

                if (result.IsSuccessStatusCode)
                {
                    TempData["message"] = "City has been added successfully";
                    return RedirectToAction("GetAll");
                }

                TempData["message"] = "City with this id is already present in the system";

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                
                var result = await _cityService.Delete(id);
                if (result.IsSuccessStatusCode)
                {
                    TempData["message"] = "The city has been deleted successfully";
                    return RedirectToAction("GetAll");
                }

                TempData["message"] = "City with this id is already present in the system";
                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

            
        }

        public IActionResult Edit(CityViewModel city)
        {
            return View(city);
        }

        [HttpPost]
        public IActionResult Edit(CityViewModel city, int id)
        {
            var result = _cityService.Edit(city, id);

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
