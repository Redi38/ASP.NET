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
        ViewData["Name"] = name ?? string.Empty;
        ViewData["Phone"] = phone ?? string.Empty;
        ViewData["Email"] = email ?? string.Empty;
        ViewData["VisitDate"] = visitDate ?? string.Empty;
        ViewData["Age"] = int.TryParse(age, out int parsedAge) ? parsedAge : 0;
        ViewData["FavoriteCuisine"] = favoriteCuisine ?? string.Empty;
        ViewData["DesiredDishes"] = desiredDishes ?? string.Empty;
        ViewData["ReasonForChoosing"] = reason ?? string.Empty;
        ViewData["Recommendation"] = recommendation ?? string.Empty;

        return View();
    }
}