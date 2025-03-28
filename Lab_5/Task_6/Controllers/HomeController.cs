using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_6.Data;
using Task_6.Models;

namespace Task_6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int step = 1, string? input = null)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (string.IsNullOrEmpty(input))
                {
                    ViewData["Error"] = "Це поле не може бути порожнім.";
                }
                else
                {
                    switch (step)
                    {
                        case 1:
                            TempData["Address"] = input;
                            break;
                        case 2:
                            TempData["Password"] = input;
                            break;
                        case 3:
                            TempData["Login"] = input;
                            break;
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
                            }
                            break;
                    }
                }
 
                return RedirectToAction("Index", new { step = step + 1 });
            }
 
            ViewData["Step"] = step;

            return View();
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}