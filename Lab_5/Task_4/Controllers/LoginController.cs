using Microsoft.AspNetCore.Mvc;

namespace Task_4.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                ViewData["Error"] = "Логін не може бути порожнім.";
                return View();
            }

            TempData["Login"] = login;
            return RedirectToAction("Index", "Email");
        }
    }
}
