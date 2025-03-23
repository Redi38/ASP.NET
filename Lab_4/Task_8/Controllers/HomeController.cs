using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

public class HomeController : Controller
{
    private string connectionString = "server=localhost;database=company_db;user=root;password=root";

    public IActionResult Index(string departmentFilter)
    {
        List<Employee> employees = new List<Employee>();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM employees";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    employees.Add(new Employee
                    {
                        e_id = reader.GetInt32("e_id"),
                        e_name = reader.GetString("e_name"),
                        e_salary = reader.GetInt32("e_salary"),
                        e_age = reader.GetInt32("e_age"),
                        e_gender = reader.GetString("e_gender"),
                        e_dept = reader.GetString("e_dept")
                    });
                }
            }
        }

        ViewData["DepartmentFilter"] = departmentFilter;
        return View(employees);
    }
}
