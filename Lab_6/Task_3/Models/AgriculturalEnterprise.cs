using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Task_3.Models
{
    [Table("AgriculturalEnterprise")]
    public class AgriculturalEnterprise
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty; 

        [Required]
        public string OwnershipType { get; set; } = string.Empty;

        [Required]
        public int LandArea { get; set; }

        [Required]
        public int EmployeesCount { get; set; }
    }
}