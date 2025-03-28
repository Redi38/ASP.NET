using Microsoft.AspNetCore.Mvc;
using Task_8.Data;

namespace Task_8.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Step"] = HttpContext.Session.GetInt32("Step") ?? 1;
            return View();
        }

        [HttpPost]
        public IActionResult Index(string address, string password)
        {
            int step = HttpContext.Session.GetInt32("Step") ?? 1;

            if (step == 1)
            {
                if (string.IsNullOrEmpty(address))
                {
                    ViewData["Error"] = "Введіть адресу.";
                    return View();
                }
                HttpContext.Session.SetString("Address", address);
                HttpContext.Session.SetInt32("Step", 2);
            }
            else if (step == 2)
            {
                if (string.IsNullOrEmpty(password))
                {
                    ViewData["Error"] = "Введіть пароль.";
                    return View();
                }
                HttpContext.Session.SetString("Password", password);

                string? savedAddress = HttpContext.Session.GetString("Address");
                var users = _context.Users
                    .Where(u => u.Address == savedAddress && u.Password == password)
                    .ToList();

                ViewData["Users"] = users;
                HttpContext.Session.Clear();
            }

            ViewData["Step"] = HttpContext.Session.GetInt32("Step") ?? 1;
            return View();
        }
    }
}
