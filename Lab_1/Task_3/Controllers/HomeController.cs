using Microsoft.AspNetCore.Mvc;
using Task_3.Data;

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
        return View();
    }

    [HttpPost]
    public IActionResult GenerateTable(int rows)
    {
        // Отримання кількості колонок із БД (або за замовчуванням 5)
        var columns = _context.ColumnCounts.FirstOrDefault()?.Count ?? 5;

        // Генерація HTML-коду таблиці
        string tableHtml = GenerateTableHtml(rows, columns);

        // Передача таблиці у ViewData
        ViewData["TableHtml"] = tableHtml;
        return View("Display");
    }

    // Генерує HTML-код шахової таблиці
    private string GenerateTableHtml(int rows, int columns)
    {
        int number = 1;
        string table = "<table class='chess-table'>";
        for (int i = 0; i < rows; i++)
        {
            table += "<tr>";
            for (int j = 0; j < columns; j++)
            {
                // Визначаємо колір комірки (чорний або білий)
                string cellClass = (i + j) % 2 == 0 ? "white-cell" : "black-cell";

                // Додаємо комірку з числом
                table += $"<td class='{cellClass}'>{number++}</td>";
            }
            table += "</tr>";
        }
        table += "</table>";
        return table;
    }
}
