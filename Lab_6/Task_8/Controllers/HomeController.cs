using Microsoft.AspNetCore.Mvc;
using Task_8.Models;

namespace Task_8.Controllers
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

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var enterprise = _context.AgriculturalEnterprises.Find(id);
            if (enterprise != null)
            {
                _context.AgriculturalEnterprises.Remove(enterprise);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var enterprise = _context.AgriculturalEnterprises.Find(id);
            if (enterprise == null)
            {
                return NotFound();
            }
            return View(enterprise);
        }

        [HttpPost]
        public IActionResult Edit(AgriculturalEnterprise model)
        {
            if (ModelState.IsValid)
            {
                _context.AgriculturalEnterprises.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
