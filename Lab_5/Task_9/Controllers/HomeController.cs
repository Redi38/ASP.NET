using Microsoft.AspNetCore.Mvc;
using Task_9.Data;

namespace Task_9.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet, HttpPost]
        public IActionResult SearchAccount(string? address, string? password)
        {
            int step = HttpContext.Session.GetInt32("Step") ?? 1;
            ViewData["Step"] = step;

            if (Request.Method == "POST")
            {
                if (step == 1)
                {
                    if (string.IsNullOrEmpty(address))
                    {
                        ViewData["Error"] = "Введіть адресу.";
                    }
                    else
                    {
                        HttpContext.Session.SetString("Address", address);
                        HttpContext.Session.SetInt32("Step", 2);
                        ViewData["Step"] = 2;
                    }
                }
                else if (step == 2)
                {
                    if (string.IsNullOrEmpty(password))
                    {
                        ViewData["Error"] = "Введіть пароль.";
                    }
                    else
                    {
                        string? savedAddress = HttpContext.Session.GetString("Address");

                        var users = _context.Users
                            .Where(u => u.Address == savedAddress && u.Password == password)
                            .ToList();

                        ViewData["Users"] = users;
                        HttpContext.Session.Clear();
                    }
                }
            }

            return View("Index");
        }
    }
}
