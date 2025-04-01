using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Task_3.Models;

namespace Task_3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var enterprises = _context.AgriculturalEnterprises.ToList();
            return View(new EnterpriseFormViewModel { Enterprises = enterprises, NewEnterprise = new AgriculturalEnterprise() });
        }

        [HttpPost]
        public IActionResult Index(EnterpriseFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.AgriculturalEnterprises.Add(model.NewEnterprise);
                _context.SaveChanges();
            }

            var enterprises = _context.AgriculturalEnterprises.ToList();
            return View(new EnterpriseFormViewModel { Enterprises = enterprises, NewEnterprise = new AgriculturalEnterprise() });
        }
    }
}
