using Microsoft.AspNetCore.Mvc;

namespace Task_1.Controllers
{
    public class TriangleController : Controller
    {
        public IActionResult Index()
        {
            double side1 = Convert.ToDouble(TempData["Side1"] ?? "0");
            double side2 = Convert.ToDouble(TempData["Side2"] ?? "0");
            double side3 = Convert.ToDouble(TempData["Side3"] ?? "0");

            ViewBag.MaxSide = new double[] { side1, side2, side3 }.Max();
            return View();
        }
    }
}
