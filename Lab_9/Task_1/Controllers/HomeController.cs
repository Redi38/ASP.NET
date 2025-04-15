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

        // Зберігаємо дані в Session
        HttpContext.Session.SetString("UserName", user.UserName!);

        return RedirectToAction("Welcome");
    }

    public IActionResult Welcome()
    {
        var userName = HttpContext.Session.GetString("UserName");
        if (userName == null) return RedirectToAction("Login");

        var user = _context.Users.FirstOrDefault(u => u.UserName == userName);
        if (user == null) return RedirectToAction("Login");

        ViewBag.UserName = user.UserName;
        ViewBag.Email = user.Email;
        ViewBag.BirthDate = user.BirthDate.ToString("yyyy-MM-dd");

        return View();
    }

    [HttpGet]
    public IActionResult Profile()
    {
        var userName = HttpContext.Session.GetString("UserName");
        if (userName == null) return RedirectToAction("Login");

        var user = _context.Users.FirstOrDefault(u => u.UserName == userName);
        if (user == null) return RedirectToAction("Login");

        var model = new UserRegistrationModel
        {
            UserName = user.UserName,
            Password = user.Password,
            ConfirmPassword = user.Password,
            Email = user.Email,
            Year = user.BirthDate.Year,
            Month = user.BirthDate.Month,
            Day = user.BirthDate.Day
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult Profile(UserRegistrationModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = _context.Users.FirstOrDefault(u => u.UserName == model.UserName);
        if (user == null)
        {
            ModelState.AddModelError("", "Користувача не знайдено.");
            return View(model);
        }

        user.Password = model.Password!;
        user.Email = model.Email!;
        user.BirthDate = new DateTime(model.Year, model.Month, model.Day);

        _context.SaveChanges();

        return RedirectToAction("Welcome");
    }
}
