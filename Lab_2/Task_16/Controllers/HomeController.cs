using Microsoft.AspNetCore.Mvc;
using Task_16.Models;

namespace Task_16.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new CitySelectionModel());
        }

        [HttpPost]
        public IActionResult ProcessSelection(CitySelectionModel model)
        {
            if (model.ListBoxCities == null || model.ListBoxCities.Count == 0)
            {
                model.ListBoxCities = new List<string> { "Нічого не вибрано" };
            }

            ViewData["SelectedCity"] = model.ComboBoxCity;
            ViewData["SelectedCities"] = string.Join(", ", model.ListBoxCities);

            return View("Index", model);
        }
    }
}
