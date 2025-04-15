using Microsoft.AspNetCore.Mvc;
using Task_4.Models;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
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

    [HttpGet]
    public IActionResult SendMessage()
    {
        if (HttpContext.Session.GetString("UserName") == null)
            return RedirectToAction("Login");

        return View(new SendMessageModel());
    }

    [HttpPost]
    public IActionResult SendMessage(SendMessageModel model)
    {
        var sender = HttpContext.Session.GetString("UserName");
        if (sender == null) return RedirectToAction("Login");

        if (!ModelState.IsValid)
            return View(model);

        var recipientExists = _context.Users.Any(u => u.UserName == model.To);
        if (!recipientExists)
        {
            ModelState.AddModelError("To", $"Користувача '{model.To}' не існує.");
            return View(model);
        }

        var message = new MessageModel
        {
            From = sender,
            To = model.To,
            Theme = model.Theme,
            Text = model.Text,
            SentAt = DateTime.Now
        };

        var path = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "messages.json");
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);

        List<MessageModel> allMessages = new();
        if (System.IO.File.Exists(path))
        {
            var existing = System.IO.File.ReadAllText(path);
            allMessages = System.Text.Json.JsonSerializer.Deserialize<List<MessageModel>>(existing) ?? new();
        }

        allMessages.Add(message);
        System.IO.File.WriteAllText(path, System.Text.Json.JsonSerializer.Serialize(allMessages));

        ViewBag.Status = "Повідомлення надіслано!";
        ModelState.Clear(); // очищення форми
        return View(new SendMessageModel());
    }

    [HttpGet]
    public IActionResult Inbox()
    {
        var currentUser = HttpContext.Session.GetString("UserName");
        if (currentUser == null) return RedirectToAction("Login");

        var path = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "messages.json");

        List<MessageModel> messages = new();
        if (System.IO.File.Exists(path))
        {
            var data = System.IO.File.ReadAllText(path);
            messages = System.Text.Json.JsonSerializer.Deserialize<List<MessageModel>>(data)!
                .Where(m => m.To == currentUser).OrderByDescending(m => m.SentAt).ToList();
        }

        return View(messages);
    }

    [HttpGet]
    public IActionResult ViewMessage(string id)
    {
        var currentUser = HttpContext.Session.GetString("UserName");
        if (currentUser == null) return RedirectToAction("Login");

        var path = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "messages.json");

        if (!System.IO.File.Exists(path)) return NotFound();

        var allMessages = System.Text.Json.JsonSerializer.Deserialize<List<MessageModel>>(
            System.IO.File.ReadAllText(path)) ?? new();

        var message = allMessages.FirstOrDefault(m => m.Id == id && m.To == currentUser);
        if (message == null) return NotFound();

        return View(message);
    }
}
