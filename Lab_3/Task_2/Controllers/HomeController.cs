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
        ViewData["Name"] = form["name"].ToString() ?? string.Empty;
        ViewData["Phone"] = form["phone"].ToString() ?? string.Empty;
        ViewData["Email"] = form["email"].ToString() ?? string.Empty;
        ViewData["VisitDate"] = form["visitDate"].ToString() ?? string.Empty;
        ViewData["Age"] = int.TryParse(form["age"], out int age) ? age : 0;
        ViewData["FavoriteCuisine"] = form["favoriteCuisine"].ToString() ?? string.Empty;
        ViewData["DesiredDishes"] = form["desiredDishes"].ToString() ?? string.Empty;
        ViewData["ReasonForChoosing"] = form["reason"].ToString() ?? string.Empty;
        ViewData["Recommendation"] = form["recommendation"].ToString() ?? string.Empty;

        return View();
    }
}
