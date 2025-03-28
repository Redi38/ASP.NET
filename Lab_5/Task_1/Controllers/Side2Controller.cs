using Microsoft.AspNetCore.Mvc;

namespace Task_1.Controllers
{
    public class Side2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveSide(double side2)
        {
            TempData["Side2"] = side2.ToString();
            return RedirectToAction("Index", "Side3");
        }
    }
}
