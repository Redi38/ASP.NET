using Microsoft.AspNetCore.Mvc;
using Task_4.Data;
using Task_4.Models;

namespace Task_4.Controllers
{
    public class EmailController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmailController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewData["Error"] = "Електронна пошта не може бути порожньою.";
                return View();
            }

            var address = TempData["Address"] as string;
            var password = TempData["Password"] as string;
            var login = TempData["Login"] as string;

            if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(login))
            {
                ViewData["Error"] = "Помилка при отриманні даних. Будь ласка, почніть спочатку.";
                return View();
            }

            var user = new User
            {
                Address = address,
                Password = password,
                Login = login,
                Email = email
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
