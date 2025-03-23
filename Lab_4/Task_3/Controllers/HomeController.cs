using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(
        string name, string phone, string email, string visitDate, string age,
        string favoriteCuisine, string desiredDishes, string reason, string recommendation)
    {

        return View();
    }
}