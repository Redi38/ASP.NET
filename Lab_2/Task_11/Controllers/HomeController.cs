using Microsoft.AspNetCore.Mvc;

namespace Task_11.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.SelectedInterests = null;
            return View();
        }

        [HttpPost]
        public IActionResult ProcessInterests(string? sport, string? travel, string? crafting, string? drawing)
        {
            List<string> selectedInterests = new();

            if (!string.IsNullOrEmpty(sport)) selectedInterests.Add("Спорт");
            if (!string.IsNullOrEmpty(travel)) selectedInterests.Add("Мандрівки");
            if (!string.IsNullOrEmpty(crafting)) selectedInterests.Add("Майстрування");
            if (!string.IsNullOrEmpty(drawing)) selectedInterests.Add("Малювання");

            ViewBag.SelectedInterests = selectedInterests.Count > 0 ? selectedInterests : null;
            return View("Index");
        }

        [HttpPost]
        public IActionResult Reset()
        {
            ViewBag.SelectedInterests = null;
            return View("Index");
        }
    }
}
