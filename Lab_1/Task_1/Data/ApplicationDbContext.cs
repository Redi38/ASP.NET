using Microsoft.EntityFrameworkCore;
using Task_1.Models;

namespace Task_1.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Конструктор для налаштування контексту бази даних
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Таблиця для збереження стилів тексту
        public DbSet<TextStyle> TextStyles { get; set; }
    }
}
