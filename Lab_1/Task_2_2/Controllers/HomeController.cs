using Microsoft.AspNetCore.Mvc;
using Task_2_2.Data;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    // Конструктор для отримання доступу до бази даних
    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Головна сторінка
    public IActionResult Index()
    {
        return View();
    }

    // Обробка форми введення URL зображень
    [HttpPost]
    public IActionResult Submit(List<string> urls)
    {
        var images = _context.Images.ToList();

        // Перевірка, чи є достатньо записів у БД
        for (int i = 0; i < images.Count && i < urls.Count; i++)
        {
            images[i].Url = urls[i];
        }

        // Передача зміненого списку в представлення
        return View("Display", images);
    }
}
