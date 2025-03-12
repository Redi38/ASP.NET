using Microsoft.AspNetCore.Mvc; 

namespace Task_6.Controllers
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
        public IActionResult Index(IFormCollection form)
        { 
            string city = form["city"].ToString() ?? "Київ";
            string transport = form["transport"].ToString() ?? "Літак"; 

            ViewBag.SelectedCity = city;
            ViewBag.SelectedTransport = transport;
            ViewBag.ShowSelection = true;

            return View();
        }
    }
}