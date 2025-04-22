using System.ComponentModel.DataAnnotations;

namespace Task_6.Models
{
    public class SendMessageModel
    {
        [Required(ErrorMessage = "Поле отримувача обов’язкове.")]
        public string To { get; set; } = "";

        [Required(ErrorMessage = "Поле теми обов’язкове.")]
        public string Theme { get; set; } = "";

        [Required(ErrorMessage = "Поле тексту повідомлення обов’язкове.")]
        public string Text { get; set; } = "";
    }
}
