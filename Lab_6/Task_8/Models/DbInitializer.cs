namespace Task_8.Models
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.AgriculturalEnterprises.Any())
            {
                return;
            }

            var enterprises = new List<AgriculturalEnterprise>
        {
            new AgriculturalEnterprise { Name = "Зоря", OwnershipType = "Д", LandArea = 300, EmployeesCount = 120 },
            new AgriculturalEnterprise { Name = "Росинка", OwnershipType = "К", LandArea = 174, EmployeesCount = 27 },
            new AgriculturalEnterprise { Name = "Петренко", OwnershipType = "П", LandArea = 56, EmployeesCount = 6 }
        };

            context.AgriculturalEnterprises.AddRange(enterprises);
            context.SaveChanges();
        }
    }
}
