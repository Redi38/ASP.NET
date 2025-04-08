using Microsoft.EntityFrameworkCore;

namespace Task_1.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<AgriculturalEnterprise> AgriculturalEnterprises { get; set; }
    }
}
