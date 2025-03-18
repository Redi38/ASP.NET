using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(double radius, string calculationType)
    {
        if (radius <= 0)
        {
            ViewData["Error"] = "Радіус має бути більше 0";
            return View();
        }

        double result = calculationType == "Volume"
            ? (4.0 / 3.0) * Math.PI * Math.Pow(radius, 3)
            : 4 * Math.PI * Math.Pow(radius, 2);

        ViewData["Radius"] = radius;
        ViewData["CalculationType"] = calculationType;
        ViewData["Result"] = result;

        return View();
    }
}
