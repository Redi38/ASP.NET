using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Task_1.Models
{
    [Table("AgriculturalEnterprise")]
    public class AgriculturalEnterprise
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? OwnershipType { get; set; }

        public int LandArea { get; set; }

        public int EmployeesCount { get; set; }
    }
}