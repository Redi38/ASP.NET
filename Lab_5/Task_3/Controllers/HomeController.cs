using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Task_3.Controllers
{
    public class HomeController : Controller
    {
        private const string SessionKey = "TriangleSides";

        public IActionResult Index(double? side, bool reset = false)
        {
            var sidesJson = HttpContext.Session.GetString(SessionKey);
            var sides = !string.IsNullOrEmpty(sidesJson)
                ? JsonConvert.DeserializeObject<List<double>>(sidesJson) ?? new List<double>()  
                : new List<double>();

            if (reset)
            {
                HttpContext.Session.Remove(SessionKey);
                return RedirectToAction("Index");
            }

            if (side.HasValue && side > 0 && sides.Count < 3)
            {
                sides.Add(side.Value);
                HttpContext.Session.SetString(SessionKey, JsonConvert.SerializeObject(sides));
            }

            ViewBag.Sides = sides;
            ViewBag.MaxSide = sides.Count == 3 ? sides.Max() : (double?)null;

            return View();
        }
    }
}