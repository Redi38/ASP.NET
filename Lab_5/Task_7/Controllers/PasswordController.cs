using Microsoft.AspNetCore.Mvc;
using Task_7.Data;

namespace Task_7.Controllers
{
    public class PasswordController : Controller
    {
        private readonly AppDbContext _context;

        public PasswordController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!TempData.ContainsKey("Address"))
            {
                return RedirectToAction("Index", "Address");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(string password)
        {
            string? address = TempData["Address"] as string;
            if (address == null) return RedirectToAction("Index", "Address");

            var user = _context.Users.FirstOrDefault(u => u.Address == address && u.Password == password);
            if (user == null)
            {
                ViewData["Error"] = "Невірний пароль.";
                return View();
            }

            return View("Result", user);
        }
    }
}
