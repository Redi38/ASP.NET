using Microsoft.AspNetCore.Mvc;
using Task_8.Models;

namespace Task_8.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new SelectionViewModel());
        }

        [HttpPost]
        public IActionResult Index(SelectionViewModel model)
        {
            model.ShowSelection = true;
            return View(model);
        }

        [HttpPost]
        public IActionResult Reset()
        {
            return View("Index", new SelectionViewModel());
        }
    }
}
