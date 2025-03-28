using Microsoft.AspNetCore.Mvc;

namespace Task_1.Controllers
{
    public class Side3Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveSide(double side3)
        {
            TempData["Side3"] = side3.ToString();
            return RedirectToAction("Index", "Triangle");
        }
    }
}
