using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Task_2_2.Models
{
    [Table("AgriculturalEnterprise")]
    public class AgriculturalEnterprise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва обов'язкова")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Тип власності обов'язковий")]
        public string OwnershipType { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Площа має бути більше 0")]
        public int LandArea { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Кількість працівників має бути більше 0")]
        public int EmployeesCount { get; set; }

        [Required(ErrorMessage = "Тип продукції обов'язковий")]
        public string ProductType { get; set; } = string.Empty;
    }
}
