using Microsoft.AspNetCore.Mvc;

/* Базовий контролер, який буде рахувати кількість відвідувань сторінки для кожного контролера */
public class BaseController : Controller
{
    public virtual IActionResult Index()
    {
        /* Генеруємо унікальний ключ для сесії на основі імені контролера */
        string key = $"VisitCount_{this.GetType().Name}";

        /* Отримуємо поточну кількість відвідувань зі сесії або 0, якщо значення ще не встановлено */
        int count = HttpContext.Session.GetInt32(key) ?? 0;
        count++; // Збільшуємо лічильник

        /* Зберігаємо оновлене значення у сесії */
        HttpContext.Session.SetInt32(key, count);

        /* Передаємо значення в ViewData для відображення у вигляді */
        ViewData["VisitCount"] = count;
        return View();
    }
}

/* Контролери для різних сторінок */
public class HomeController : BaseController { }
public class Page2Controller : BaseController { }
public class Page3Controller : BaseController { }
public class Page4Controller : BaseController { }
public class Page5Controller : BaseController { }
