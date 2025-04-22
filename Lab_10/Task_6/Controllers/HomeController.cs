using Microsoft.AspNetCore.Mvc;
using Task_6.Models;

public class HomeController : Controller
{

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
        var users = DataStorage.LoadUsers();

        if (users.Any(u => u.UserName == model.UserName))
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

        users.Add(user);
        DataStorage.SaveUsers(users);

        TempData["Success"] = "Реєстрація успішна! Увійдіть до системи.";
        return RedirectToAction("Login");
    }


    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(LoginModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var users = DataStorage.LoadUsers();
        var user = users.FirstOrDefault(u => u.UserName == model.Username);
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

        HttpContext.Session.SetString("UserName", user.UserName!);
        return RedirectToAction("Welcome");
    }

    public IActionResult Welcome()
    {
        var userName = HttpContext.Session.GetString("UserName");
        if (userName == null)
            return RedirectToAction("Login");

        var user = DataStorage.LoadUsers().FirstOrDefault(u => u.UserName == userName);
        if (user == null)
            return RedirectToAction("Login");

        ViewBag.UserName = user.UserName;
        ViewBag.Email = user.Email;
        ViewBag.BirthDate = user.BirthDate.ToString("yyyy-MM-dd");
        ViewBag.TestScore = user.TestScore;
        ViewBag.TestAttempts = user.TestAttempts;
        ViewBag.MaxAttempts = AdminStorage.MaxTestAttempts;
        ViewBag.IsAdmin = user.IsAdmin;

        return View();
    }


    [HttpGet]
    public IActionResult Profile()
    {
        var username = HttpContext.Session.GetString("UserName");
        if (username == null) return RedirectToAction("Login");

        var user = DataStorage.LoadUsers().FirstOrDefault(u => u.UserName == username);
        if (user == null) return RedirectToAction("Login");

        return View(new UserRegistrationModel
        {
            UserName = user.UserName,
            Password = user.Password,
            ConfirmPassword = user.Password,
            Email = user.Email,
            Year = user.BirthDate.Year,
            Month = user.BirthDate.Month,
            Day = user.BirthDate.Day
        });
    }

    [HttpPost]
    public IActionResult Profile(UserRegistrationModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var users = DataStorage.LoadUsers();
        var user = users.FirstOrDefault(u => u.UserName == model.UserName);
        if (user == null)
        {
            ModelState.AddModelError("", "Користувач не знайдений.");
            return View(model);
        }

        user.Password = model.Password!;
        user.Email = model.Email!;
        user.BirthDate = new DateTime(model.Year, model.Month, model.Day);

        DataStorage.SaveUsers(users);
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

        var users = DataStorage.LoadUsers();
        var recipientExists = users.Any(u => u.UserName == model.To);
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

        var allMessages = DataStorage.LoadMessages();
        allMessages.Add(message);
        DataStorage.SaveMessages(allMessages);

        ViewBag.Status = "Повідомлення надіслано!";
        ModelState.Clear();
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
