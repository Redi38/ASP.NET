using Microsoft.AspNetCore.Mvc;

/* Базовий контролер для всіх сторінок */
public class BaseController : Controller
{
    /* Відображення головної сторінки */
    public IActionResult Index()
    {
        return View();
    }
}

/* Контролери для різних сторінок */
public class HomeController : BaseController { }
public class Page2Controller : BaseController { }
public class Page3Controller : BaseController { }
public class Page4Controller : BaseController { }
public class Page5Controller : BaseController { }
