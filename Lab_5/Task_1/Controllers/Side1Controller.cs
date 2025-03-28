using Microsoft.AspNetCore.Mvc;

namespace Task_1.Controllers
{
    public class Side1Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveSide(double side1)
        {
            TempData["Side1"] = side1.ToString();
            return RedirectToAction("Index", "Side2");
        }
    }
}
