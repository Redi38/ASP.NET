using Microsoft.AspNetCore.Mvc;

namespace Task_13.Controllers
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
        public IActionResult ProcessSelection()
        {
            string selectedCity = Request.Form["comboBoxCities"].FirstOrDefault() ?? "";
            string[] selectedCities = Request.Form["listBoxCities"].ToArray();

            ViewData["SelectedCity"] = selectedCity;
            ViewData["SelectedCities"] = selectedCities;

            return View("Index");
        }
    }
}
