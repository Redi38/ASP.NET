using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Task_10.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["SelectedValues"] = new string[] { "Спорт" };
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormCollection form)
        {
            var selectedInterests = form["interests"].Where(interest => !string.IsNullOrEmpty(interest)).ToArray();

            if (selectedInterests.Length == 0)
            {
                selectedInterests = new string[] { "Спорт" };
            }

            ViewData["SelectedInterests"] = string.Join(", ", selectedInterests);
            ViewData["SelectedValues"] = selectedInterests;

            return View();
        }
    }
}
