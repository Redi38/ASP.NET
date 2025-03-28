using Microsoft.AspNetCore.Mvc;

namespace Task_4.Controllers
{
    public class AddressController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                ViewData["Error"] = "Адреса не може бути порожньою.";
                return View();
            }

            TempData["Address"] = address;
            return RedirectToAction("Index", "Password");
        }
    }
}
