using Microsoft.EntityFrameworkCore;
using Task_6.Data;

var builder = WebApplication.CreateBuilder(args);

// Додаємо службу сесій
builder.Services.AddSession();

// Додаємо MVC
builder.Services.AddControllersWithViews();

// Налаштування підключення до MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Використовуємо сесію
app.UseSession();

// Додаємо підтримку статичних файлів (CSS, JS, зображення)
app.UseStaticFiles();

// Налаштування маршрутизації
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
