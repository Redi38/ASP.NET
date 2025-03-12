using Microsoft.AspNetCore.Mvc;
using Task_12.Models;

namespace Task_12.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new InterestModel());
        }

        [HttpPost]
        public IActionResult Index(InterestModel model)
        {
            List<string> selectedInterests = new();

            if (model.Sport) selectedInterests.Add("Спорт");
            if (model.Travel) selectedInterests.Add("Мандрівки");
            if (model.Crafting) selectedInterests.Add("Майстрування");
            if (model.Drawing) selectedInterests.Add("Малювання");

            ViewData["SelectedInterests"] = selectedInterests.Count > 0
                ? string.Join(", ", selectedInterests)
                : null;

            return View(model);
        }

        [HttpPost]
        public IActionResult Reset()
        {
            return View("Index", new InterestModel());
        }
    }
}
