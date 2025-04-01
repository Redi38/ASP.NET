using Microsoft.AspNetCore.Mvc;
using Task_2.Models;

namespace Task_2.Controllers
{
    public class HomeController : Controller
    {
        private static List<AgriculturalEnterprise> _enterprises = new();

        [HttpGet]
        public IActionResult Index()
        {
            return View(new EnterpriseFormViewModel { Enterprises = _enterprises, NewEnterprise = new AgriculturalEnterprise() });
        }

        [HttpPost]
        public IActionResult Index(EnterpriseFormViewModel model)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(model.NewEnterprise.Name))
            {
                _enterprises.Add(model.NewEnterprise);
            }

            return View(new EnterpriseFormViewModel { Enterprises = _enterprises, NewEnterprise = new AgriculturalEnterprise() });
        }
    }
}
