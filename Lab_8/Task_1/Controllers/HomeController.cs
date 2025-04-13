using Microsoft.AspNetCore.Mvc;
using Task_1.Models;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public IActionResult Register(UserRegistrationModel model)
    {
        if (_context.Users.Any(u => u.UserName == model.UserName))
        {
            ModelState.AddModelError("UserName", "Користувач вже існує.");
        }

        if (!ModelState.IsValid)
            return View(model);

        var user = new User
        {
            UserName = model.UserName,
            Password = model.Password,
            Email = model.Email,
            BirthDate = new DateTime(model.Year, model.Month, model.Day)
        };

        _context.Users.Add(user);
        _context.SaveChanges();
        return RedirectToAction("Success");
    }

    public IActionResult Success() => View();
}
