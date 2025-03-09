using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_1.Data;
using Task_1.Models;

namespace Task_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Конструктор для отримання доступу до бази даних
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Головна сторінка, отримує стилі з БД та передає у ViewBag
        public IActionResult Index()
        {
            var styles = _context.TextStyles.ToList();

            if (styles.Count < 3)
            {
                return Content("В базі недостатньо стилів для відображення.");
            }

            ViewBag.Styles = styles;
            return View();
        }

        // Метод для відображення введеного тексту з обраними стилями
        [HttpPost]
        public IActionResult Display(string paragraph1, string paragraph2, string paragraph3, string textColor, string fontSize, string fontStyle, string textAlign)
        {
            var styles = _context.TextStyles.ToList();
            ViewBag.Paragraphs = new List<string> { paragraph1, paragraph2, paragraph3 };
            ViewBag.Styles = styles;
            ViewBag.TextColor = textColor;
            ViewBag.FontSize = fontSize;
            ViewBag.FontStyle = fontStyle;
            ViewBag.TextAlign = textAlign;
            return View();
        }
    }
}
