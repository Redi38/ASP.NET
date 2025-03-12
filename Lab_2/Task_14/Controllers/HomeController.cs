using Microsoft.AspNetCore.Mvc;

namespace Task_14.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["SelectedCity"] = null;
            ViewData["SelectedCities"] = new string[] { };

            return View();
        }

        [HttpPost]
        public IActionResult ProcessSelection(IFormCollection form)
        {
            string selectedCity = form["comboBoxCities"].FirstOrDefault() ?? "";
            string[] selectedCities = form["listBoxCities"].ToArray();

            ViewData["SelectedCity"] = selectedCity;
            ViewData["SelectedCities"] = selectedCities;

            return View("Index");
        }
    }
}
