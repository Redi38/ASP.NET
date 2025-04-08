namespace Task_2_2.Models
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
            new AgriculturalEnterprise { Name = "Зоря", OwnershipType = "Д", LandArea = 300, EmployeesCount = 120, ProductType = "Зернові" },
            new AgriculturalEnterprise { Name = "Росинка", OwnershipType = "К", LandArea = 174, EmployeesCount = 27, ProductType = "Овочі" },
            new AgriculturalEnterprise { Name = "Петренко", OwnershipType = "П", LandArea = 56, EmployeesCount = 6, ProductType = "Фрукти" }
        };

            context.AgriculturalEnterprises.AddRange(enterprises);
            context.SaveChanges();
        }
    }
}
