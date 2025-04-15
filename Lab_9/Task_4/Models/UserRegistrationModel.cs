using System.ComponentModel.DataAnnotations;

namespace Task_4.Models
{
    public class UserRegistrationModel
    {

        [Required(ErrorMessage = "Поле 'Ім'я користувача' є обов'язковим.")]
        [MaxLength(15)]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Тільки латинські літери та цифри.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Поле 'Пароль' є обов'язковим.")]
        [MinLength(12, ErrorMessage = "Пароль повинен містити щонайменше 12 символів.")]
        [RegularExpression(@"^(?=.*[!@#$%^&*()_\-+=]).+$", ErrorMessage = "Пароль має містити спецсимвол.")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Паролі не співпадають.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Поле 'Електронна пошта' є обов'язковим.")]
        [EmailAddress(ErrorMessage = "Некоректна пошта.")]
        public string? Email { get; set; }

        [Range(1900, 2025)]
        public int Year { get; set; }

        [Range(1, 12)]
        public int Month { get; set; }

        [Range(1, 31)]
        public int Day { get; set; }
    }
}