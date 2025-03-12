using Microsoft.AspNetCore.Mvc;

namespace Task_5.Controllers
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
        public IActionResult ProcessSelection()
        {
            string city = Request.Form["city"].FirstOrDefault() ?? "Київ";
            string transport = Request.Form["transport"].FirstOrDefault() ?? "Літак";

            ViewBag.SelectedCity = city;
            ViewBag.SelectedTransport = transport;
            ViewBag.ShowSelection = true;

            return View("Index");
        }
    }
}
