using Microsoft.AspNetCore.Mvc;
using TaxiBookingServiceUI.Models;
using TaxiBookingServiceUI.Services;

namespace TaxiBookingServiceUI.Controllers
{
    public class AreaController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AreaService _areaService;
        private readonly CityService _cityService;
        private readonly StateService _stateService;

        public AreaController(IHttpContextAccessor httpContextAccessor, CityService cityService, AreaService areaService , StateService stateService)
        {
            _contextAccessor = httpContextAccessor;
            _areaService = areaService;
            _cityService = cityService;
            _stateService = stateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return View(await _areaService.GetAll());
        }

        [HttpGet]
        public IActionResult GetAreas(string filter)
        {
            return Json(_areaService.GetAreas(filter));
        }

        [HttpGet]
        public IActionResult Add()
        {
            try
            {
                List<string> allCities = _cityService.GetCityNames();

                ViewBag.allCities = allCities;

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
           
        }

        [HttpPost]
        public IActionResult Add(AreaViewModel area)
        {
            try
            {
                List<string> allCities = _cityService.GetCityNames();

                ViewBag.allCities = allCities;

                string userName = HttpContext.Session.GetString("username");

                var result = _areaService.Add(area, userName);

                if (result.IsSuccessStatusCode)
                {
                    TempData["message"] = "Area has been added successfully";
                    return RedirectToAction("GetAll");
                }

                TempData["message"] = "Area with this id is already present in the system";

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }

        public IActionResult Delete(int id)
        {
            try
            {
                TempData["message"] = "The book has been deleted successfully";
                _areaService.Delete(id);
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }

            return RedirectToAction("GetAll");
        }

        public IActionResult Edit(AreaViewModel area)
        {
            return View(area);
        }

        [HttpPost]
        public IActionResult Edit(AreaViewModel area, int id)
        {
            var result = _areaService.Edit(area, id);

            if (result.IsSuccessStatusCode)
            {
                TempData["message"] = "The area has been updated successfully";
                return RedirectToAction("GetAll");
            }

            TempData["message"] = "The area cannot be updated";
            return View();
        }
    }
}

