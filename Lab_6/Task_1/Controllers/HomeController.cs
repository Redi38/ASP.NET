using Microsoft.AspNetCore.Mvc;
using Task_1.Models;

namespace Task_1.Controllers
{
    public class HomeController : Controller
    {
        private static List<AgriculturalEnterprise> _enterprises = new();

        [HttpGet]
        public IActionResult Index()
        {
            return View(_enterprises);
        }

        [HttpPost]
        public IActionResult Index(string name, string ownershipType, int landArea, int employeesCount)
        {
            var enterprise = new AgriculturalEnterprise
            {
                Name = name,
                OwnershipType = ownershipType,
                LandArea = landArea,
                EmployeesCount = employeesCount
            };

            _enterprises.Add(enterprise);
            return View(_enterprises);
        }
    }
}
