    using Microsoft.AspNetCore.Mvc;
    using Task_1.Models;

    namespace Task_1.Controllers
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
                return View(new EnterpriseFormViewModel
                {
                    Enterprises = enterprises,
                    NewEnterprise = new AgriculturalEnterprise()
                });
            }

            [HttpPost]
            public IActionResult Index(EnterpriseFormViewModel model)
            {
                var newEnterprise = model.NewEnterprise;

                if (string.IsNullOrWhiteSpace(newEnterprise.Name))
                    ModelState.AddModelError("NewEnterprise.Name", "Назва обов'язкова");

                if (string.IsNullOrWhiteSpace(newEnterprise.OwnershipType) ||
                    !"ДПК".Contains(newEnterprise.OwnershipType))
                    ModelState.AddModelError("NewEnterprise.OwnershipType", "Недопустимий тип власності");

                if (newEnterprise.LandArea <= 0)
                    ModelState.AddModelError("NewEnterprise.LandArea", "Площа повинна бути більшою за 0");

                if (newEnterprise.EmployeesCount < 1)
                    ModelState.AddModelError("NewEnterprise.EmployeesCount", "Кількість працівників повинна бути не менше 1");

                if (ModelState.IsValid)
                {
                    _context.AgriculturalEnterprises.Add(newEnterprise);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

                model.Enterprises = _context.AgriculturalEnterprises.ToList();
                return View(model);
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
                    return NotFound();

                return View(enterprise);
            }

            [HttpPost]
            public IActionResult Edit(AgriculturalEnterprise model)
            {
                if (string.IsNullOrWhiteSpace(model.Name))
                    ModelState.AddModelError("Name", "Назва обов'язкова");

                if (string.IsNullOrWhiteSpace(model.OwnershipType) ||
                    !"ДПК".Contains(model.OwnershipType))
                    ModelState.AddModelError("OwnershipType", "Недопустимий тип власності");

                if (model.LandArea <= 0)
                    ModelState.AddModelError("LandArea", "Площа повинна бути більшою за 0");

                if (model.EmployeesCount < 1)
                    ModelState.AddModelError("EmployeesCount", "Кількість працівників повинна бути не менше 1");

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
