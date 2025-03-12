using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Task_9.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["SelectedValues"] = "Спорт";
            return View();
        }

        [HttpPost]
        public IActionResult ProcessInterests()
        {
            var interests = Request.Form["interests"];

            string selectedInterests = interests.Where(i => !string.IsNullOrEmpty(i)).Any()
                ? string.Join(", ", interests.Where(i => !string.IsNullOrEmpty(i)))
                : "Спорт";

            ViewData["SelectedInterests"] = selectedInterests;
            ViewData["SelectedValues"] = selectedInterests;

            return View("Index");
        }
    }
}
