using Microsoft.EntityFrameworkCore;

namespace Task_2_2.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Таблиця для збереження зображень
        public DbSet<ImageModel> Images { get; set; }

        // Конструктор для налаштування контексту бази даних
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
