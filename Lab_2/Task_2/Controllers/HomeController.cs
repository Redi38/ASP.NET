using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(IFormCollection form)
    {
        string? name = form["name"];
        string? phone = form["phone"];
        string? email = form["email"];
        string? birthdate = form["birthdate"];

        ViewBag.Name = name ?? "Не вказано";
        ViewBag.Phone = phone ?? "Не вказано";
        ViewBag.Email = email ?? "Не вказано";
        ViewBag.Birthdate = birthdate ?? "Не вказано";

        return View();
    }
}