using Microsoft.AspNetCore.Mvc;
using Task_4.Models;

namespace Task_4.Controllers
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

        [HttpPost]
        public IActionResult DeleteAll()
        {
            _context.AgriculturalEnterprises.RemoveRange(_context.AgriculturalEnterprises);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
