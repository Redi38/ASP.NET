using Microsoft.AspNetCore.Mvc;
using Task_4.Models;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(UserFormModel model)
    {
        ViewBag.Name = !string.IsNullOrEmpty(model.Name) ? model.Name : "Не вказано";
        ViewBag.Phone = !string.IsNullOrEmpty(model.Phone) ? model.Phone : "Не вказано";
        ViewBag.Email = !string.IsNullOrEmpty(model.Email) ? model.Email : "Не вказано";
        ViewBag.Birthdate = !string.IsNullOrEmpty(model.Birthdate) ? model.Birthdate : "Не вказано";

        return View();
    }
}