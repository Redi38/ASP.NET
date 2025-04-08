using Microsoft.EntityFrameworkCore;

namespace Task_1_2.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<AgriculturalEnterprise> AgriculturalEnterprises { get; set; }
    }
}
