using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_2.Models
{
    [Table("AgriculturalEnterprise")]
    public class AgriculturalEnterprise
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва є обов’язковою.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Оберіть тип власності.")]
        public string OwnershipType { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Площа має бути більшою за 0.")]
        public int LandArea { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Кількість працівників має бути більшою за 0.")]
        public int EmployeesCount { get; set; }
    }
}