using Microsoft.EntityFrameworkCore;

namespace Task_8.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<AgriculturalEnterprise> AgriculturalEnterprises { get; set; }
    }
}
