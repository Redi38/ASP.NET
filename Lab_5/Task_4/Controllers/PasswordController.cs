using Microsoft.AspNetCore.Mvc;

namespace Task_4.Controllers
{
    public class PasswordController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                ViewData["Error"] = "Пароль не може бути порожнім.";
                return View();
            }

            TempData["Password"] = password;
            return RedirectToAction("Index", "Login");
        }
    }
}
