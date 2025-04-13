using Microsoft.AspNetCore.Mvc;
using Task_2.Models;

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
            return View(model);
        }

        if (!ModelState.IsValid)
            return View(model);

        var user = new User
        {
            UserName = model.UserName!,
            Password = model.Password!,
            Email = model.Email!,
            BirthDate = new DateTime(model.Year, model.Month, model.Day)
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        TempData["Success"] = "Реєстрація успішна!";
        return RedirectToAction("Success");
    }

    public IActionResult Success() => View();

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(LoginModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = _context.Users.FirstOrDefault(u => u.UserName == model.Username);
        if (user == null)
        {
            ViewBag.ErrorUsername = "Користувача не знайдено.";
            return View(model);
        }

        if (user.Password != model.Password)
        {
            ViewBag.ErrorPassword = "Невірний пароль.";
            return View(model);
        }

        TempData["UserName"] = user.UserName;
        TempData["Email"] = user.Email;
        TempData["BirthDate"] = user.BirthDate.ToString("yyyy-MM-dd");

        return RedirectToAction("Welcome");
    }

    public IActionResult Welcome()
    {
        ViewBag.UserName = TempData["UserName"];
        ViewBag.Email = TempData["Email"];
        ViewBag.BirthDate = TempData["BirthDate"];
        return View();
    }
}