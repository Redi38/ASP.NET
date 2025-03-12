using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (Request.Method == "POST")
        {
            string? name = Request.Form["name"];
            string? phone = Request.Form["phone"];
            string? email = Request.Form["email"];
            string? birthdate = Request.Form["birthdate"];

            ViewBag.Name = name ?? "Не вказано";
            ViewBag.Phone = phone ?? "Не вказано";
            ViewBag.Email = email ?? "Не вказано";
            ViewBag.Birthdate = birthdate ?? "Не вказано";
        }

        return View();
    }
}
