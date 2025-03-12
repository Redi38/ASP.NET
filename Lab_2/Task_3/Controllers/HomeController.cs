using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (Request.Method == "POST")
        {
            var form = Request.Form;
            string name = form["name"].ToString();
            string phone = form["phone"].ToString();
            string email = form["email"].ToString();
            string birthdate = form["birthdate"].ToString();

            ViewBag.Name = !string.IsNullOrEmpty(name) ? name : "Не вказано";
            ViewBag.Phone = !string.IsNullOrEmpty(phone) ? phone : "Не вказано";
            ViewBag.Email = !string.IsNullOrEmpty(email) ? email : "Не вказано";
            ViewBag.Birthdate = !string.IsNullOrEmpty(birthdate) ? birthdate : "Не вказано";
        }

        return View();
    }
}
