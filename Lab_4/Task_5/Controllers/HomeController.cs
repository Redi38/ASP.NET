using Microsoft.AspNetCore.Mvc;
using Task_5.Models;

public class HomeController : Controller
{
    private const string VisitCounterKey = "VisitCounter";

    [HttpGet]
    public IActionResult Index()
    {
        int visitCounter = HttpContext.Session.GetInt32(VisitCounterKey) ?? 1;
        ViewData["VisitCounter"] = visitCounter;

        return View(new SurveyModel());
    }

    [HttpPost]
    public IActionResult Index(SurveyModel model, string action)
    {
        int visitCounter = HttpContext.Session.GetInt32(VisitCounterKey) ?? 1;

        if (action == "Очистити")
        {
            visitCounter++;
            HttpContext.Session.SetInt32(VisitCounterKey, visitCounter);
            return RedirectToAction("Index");
        }

        ViewData["VisitCounter"] = visitCounter;
        return View(model);
    }
}
