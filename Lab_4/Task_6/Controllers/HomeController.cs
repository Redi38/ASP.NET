using Microsoft.AspNetCore.Mvc;
using Task_6.Models;

public class HomeController : Controller
{
    private const string VisitCounterKey = "VisitCounter";

    [HttpGet, HttpPost]
    public IActionResult Index(SurveyModel model, string action)
    {
        int visitCounter = HttpContext.Session.GetInt32(VisitCounterKey) ?? 1;

        if (Request.Method == "POST")
        {
            if (action == "Очистити")
            {
                visitCounter++;
                HttpContext.Session.SetInt32(VisitCounterKey, visitCounter);
                model = new SurveyModel();
            }
        }

        ViewData["VisitCounter"] = visitCounter;
        return View(model);
    }
}
