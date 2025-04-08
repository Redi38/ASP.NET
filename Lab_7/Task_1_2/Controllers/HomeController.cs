using Microsoft.AspNetCore.Mvc;
using Task_1_2.Models;

namespace Task_1_2.Controllers
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
            // Валідація через контролер
            if (string.IsNullOrWhiteSpace(model.NewEnterprise.Name))
                ModelState.AddModelError("NewEnterprise.Name", "Назва є обов’язковою");

            if (string.IsNullOrWhiteSpace(model.NewEnterprise.OwnershipType))
                ModelState.AddModelError("NewEnterprise.OwnershipType", "Вид власності є обов’язковим");

            if (model.NewEnterprise.LandArea <= 0)
                ModelState.AddModelError("NewEnterprise.LandArea", "Площа має бути більшою за 0");

            if (model.NewEnterprise.EmployeesCount < 1)
                ModelState.AddModelError("NewEnterprise.EmployeesCount", "Кількість працівників повинна бути не менше 1");

            if (string.IsNullOrWhiteSpace(model.NewEnterprise.ProductType))
                ModelState.AddModelError("NewEnterprise.ProductType", "Тип продукції є обов’язковим");

            if (ModelState.IsValid)
            {
                _context.AgriculturalEnterprises.Add(model.NewEnterprise);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var enterprises = _context.AgriculturalEnterprises.ToList();
            return View(new EnterpriseFormViewModel { Enterprises = enterprises, NewEnterprise = model.NewEnterprise });
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
            // Валідація через контролер
            if (string.IsNullOrWhiteSpace(model.Name))
                ModelState.AddModelError("Name", "Назва є обов’язковою");

            if (string.IsNullOrWhiteSpace(model.OwnershipType))
                ModelState.AddModelError("OwnershipType", "Вид власності є обов’язковим");

            if (model.LandArea <= 0)
                ModelState.AddModelError("LandArea", "Площа має бути більшою за 0");

            if (model.EmployeesCount < 1)
                ModelState.AddModelError("EmployeesCount", "Кількість працівників повинна бути не менше 1");

            if (string.IsNullOrWhiteSpace(model.ProductType))
                ModelState.AddModelError("ProductType", "Тип продукції є обов’язковим");

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
