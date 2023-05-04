using Microsoft.AspNetCore.Mvc;
using TaxiBookingServiceUI.Models;
using TaxiBookingServiceUI.Services;

namespace TaxiBookingServiceUI.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly RoleService _roleService;
        public UserRoleController(IHttpContextAccessor httpContextAccessor, RoleService roleService)
        {
            _contextAccessor = httpContextAccessor;
            _roleService = roleService;
        }

        public async Task<IActionResult> GetAll()
        {
            return View(await _roleService.GetAll());
        }

        [HttpGet]
        public IActionResult Add()
        {
            try
            {
                List<string> roleNames = _roleService.GetRoleNames();

                ViewBag.roleNames = roleNames;

                return View();
            }
            catch(Exception ex)
            {
                return View(ex.Message);
            }
           
        }

        [HttpPost]
        public IActionResult Add(RoleViewModel role)
        {
            string userName = HttpContext.Session.GetString("username");

            var result = _roleService.Add(role, userName);

            if (result.IsSuccessStatusCode)
            {
                TempData["message"] = "Role has been added successfully";
                return RedirectToAction("GetAll");
            }

            TempData["message"] = "Role with this id is already present in the system";

            return View();
        }

        public IActionResult Delete(int id)
        {
            try
            {
                TempData["message"] = "The Role has been deleted successfully";
                _roleService.Delete(id);
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }

            return RedirectToAction("GetAll");
        }

        public IActionResult Edit(CityViewModel city)
        {
            return View(city);
        }

        [HttpPost]
        public IActionResult Edit(RoleViewModel city, int id)
        {
            var result = _roleService.Edit(city, id);

            if (result.IsSuccessStatusCode)
            {
                TempData["message"] = "The Role has been updated successfully";
                return RedirectToAction("GetAll");
            }

            TempData["message"] = "The Role cannot be updated";
            return View();
        }
    }
}
