namespace Task_1.Models
{
    public class AgriculturalEnterprise
    {
        public required string Name { get; set; }
        public required string OwnershipType { get; set; }
        public int LandArea { get; set; }
        public int EmployeesCount { get; set; }
    }
}