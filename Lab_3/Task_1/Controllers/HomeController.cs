using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult IndexPost()
    {
        var name = Request.Form["name"].ToString() ?? string.Empty;
        var phone = Request.Form["phone"].ToString() ?? string.Empty;
        var email = Request.Form["email"].ToString() ?? string.Empty;
        var visitDate = Request.Form["visitDate"].ToString() ?? string.Empty;
        var age = int.TryParse(Request.Form["age"], out int parsedAge) ? parsedAge : 0;
        var favoriteCuisine = Request.Form["favoriteCuisine"].ToString() ?? string.Empty;
        var desiredDishes = Request.Form["desiredDishes"].ToString() ?? string.Empty;
        var reasonForChoosing = Request.Form["reason"].ToString() ?? string.Empty;
        var recommendation = Request.Form["recommendation"].ToString() ?? string.Empty;

        ViewData["Name"] = name;
        ViewData["Phone"] = phone;
        ViewData["Email"] = email;
        ViewData["VisitDate"] = visitDate;
        ViewData["Age"] = age;
        ViewData["FavoriteCuisine"] = favoriteCuisine;
        ViewData["DesiredDishes"] = desiredDishes;
        ViewData["ReasonForChoosing"] = reasonForChoosing;
        ViewData["Recommendation"] = recommendation;

        return View("Index");
    }
}