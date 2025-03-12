using Microsoft.AspNetCore.Mvc;

namespace Task_15.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["SelectedCity"] = null;
            ViewData["SelectedCities"] = null;

            return View();
        }

        [HttpPost]
        public IActionResult ProcessSelection(string? comboBoxCities, string[]? listBoxCities)
        {
            string selectedCity = comboBoxCities ?? "Братислава";
            string[] selectedCities = listBoxCities ?? new string[] { };

            ViewData["SelectedCity"] = selectedCity;
            ViewData["SelectedCities"] = selectedCities;

            return View("Index");
        }
    }
}
