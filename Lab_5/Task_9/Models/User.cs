using System.ComponentModel.DataAnnotations;

namespace Task_9.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string Login { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}