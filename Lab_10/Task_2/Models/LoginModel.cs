using System.ComponentModel.DataAnnotations;

namespace Task_2.Models
{
    public class LoginModel
    {
        [Required]
        [MaxLength(15)]
        public string? Username { get; set; }

        [Required]
        [MinLength(12)]
        public string? Password { get; set; }
    }
}