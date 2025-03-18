using Microsoft.AspNetCore.Mvc;
using Task_4.Models;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View(new SurveyModel());
    }

    [HttpPost]
    public IActionResult Index([FromForm] SurveyModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        return View(model);
    }
}
