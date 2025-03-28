using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_5.Data;
using Task_5.Models;

namespace Task_5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int step = 1)
        {
            ViewData["Step"] = step;
            return View();
        }

        [HttpPost]
        public IActionResult Index(int step, string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                ViewData["Error"] = "Це поле не може бути порожнім.";
                ViewData["Step"] = step;
                return View();
            }

            switch (step)
            {
                case 1:
                    TempData["Address"] = input;
                    return RedirectToAction("Index", new { step = 2 });
                case 2:
                    TempData["Password"] = input;
                    return RedirectToAction("Index", new { step = 3 });
                case 3:
                    TempData["Login"] = input;
                    return RedirectToAction("Index", new { step = 4 });
                case 4:
                    TempData["Email"] = input;

                    var user = new User
                    {
                        Address = TempData["Address"] as string ?? string.Empty,
                        Password = TempData["Password"] as string ?? string.Empty,
                        Login = TempData["Login"] as string ?? string.Empty,
                        Email = TempData["Email"] as string ?? string.Empty
                    };

                    try
                    {
                        _context.Users.Add(user);
                        _context.SaveChanges();
                        return RedirectToAction("Success");
                    }
                    catch (DbUpdateException ex)
                    {
                        var innerException = ex.InnerException?.Message ?? "No inner exception";
                        ViewData["Error"] = $"Не вдалося зберегти дані користувача: {innerException}";
                        ViewData["Step"] = step;
                        return View();
                    }
            }

            return View();
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}