using Microsoft.AspNetCore.Mvc;
using Task_2.Extensions;


namespace Task_2.Controllers
{
    public class HomeController : Controller
    {
        private const string SessionKey = "TriangleSides";

        public IActionResult Index()
        {
            List<double> sides = HttpContext.Session.GetObjectFromJson<List<double>>(SessionKey) ?? new List<double>();
            ViewBag.Sides = sides;
            ViewBag.MaxSide = sides.Count == 3 ? sides.Max() : (double?)null;
            return View();
        }

        [HttpPost]
        public IActionResult AddSide(double side)
        {
            if (side <= 0)
            {
                ModelState.AddModelError("", "Сторона повинна бути більше 0.");
                return RedirectToAction("Index");
            }

            List<double> sides = HttpContext.Session.GetObjectFromJson<List<double>>(SessionKey) ?? new List<double>();

            if (sides.Count < 3)
            {
                sides.Add(side);
                HttpContext.Session.SetObjectAsJson(SessionKey, sides);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Reset()
        {
            HttpContext.Session.Remove(SessionKey);
            return RedirectToAction("Index");
        }
    }
}
