using Microsoft.AspNetCore.Mvc;

namespace Task_7.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.SelectedCity = "Київ";
            ViewBag.SelectedTransport = "Літак";
            ViewBag.ShowSelection = false;
            return View();
        }

        [HttpPost]
        public IActionResult Index(string? city, string? transport)
        {
            ViewBag.SelectedCity = city ?? "Київ";
            ViewBag.SelectedTransport = transport ?? "Літак";
            ViewBag.ShowSelection = true;

            return View();
        }
    }
}
