using Microsoft.AspNetCore.Mvc;
using Task_7.Data;

namespace Task_7.Controllers
{
    public class AddressController : Controller
    {
        private readonly AppDbContext _context;

        public AddressController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string address)
        {
            var user = _context.Users.FirstOrDefault(u => u.Address == address);
            if (user == null)
            {
                ViewData["Error"] = "Користувача не знайдено за вказаною адресою.";
                return View();
            }

            TempData["Address"] = address;
            return RedirectToAction("Index", "Password");
        }
    }
}
